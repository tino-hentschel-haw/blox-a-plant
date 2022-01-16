using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

#endif

namespace blox.procedural
{
    [RequireComponent(typeof(MeshFilter))]
    public class BezierMeshGenerator : MonoBehaviour
    {
        [SerializeField] private Mesh2D shape2D;
        [Range(2, 32)] [SerializeField] private int edgeRingCount = 8;

        [SerializeField] private float edgeRingScale = 1.0f;
        [SerializeField] private float edgeRingScaleFactorPerSegment = 0.0f;
        
        [Header("Bezier Control Points")] [SerializeField]
        private Transform bezierStart;

        [SerializeField] private Transform bezierStartTangent;
        [SerializeField] private Transform bezierEnd;
        [SerializeField] private Transform bezierEndTangent;

        [Header("Debug Gizmos")]
        [Range(0, 1)] [SerializeField] private float tTest = 0;

        [SerializeField] private float controlPointRadius = 0.1f;
        
        private Mesh mesh;

        private void Awake()
        {
            mesh = new Mesh {name = "BezierMeshGenerator"};
            GetComponent<MeshFilter>().sharedMesh = mesh;
        }

        private void Update() => GenerateMesh();

        private void GenerateMesh()
        {
            mesh.Clear();

            // Vertices & Normals
            var verts = new List<Vector3>();
            var normals = new List<Vector3>();

            for (var ring = 0; ring < edgeRingCount; ring++)
            {
                var t = ring / (edgeRingCount - 1f);
                var orientedPoint = GetBezierOrientedPoint(t);
                
                // Scaling the edgeRings works but could probably be done better
                var currentEdgeRingScale = edgeRingScaleFactorPerSegment <= 0.0f ? edgeRingScale : edgeRingScale * edgeRingScaleFactorPerSegment * ring;

                for (var i = 0; i < shape2D.VertexCount; i++)
                {
                    verts.Add(orientedPoint.LocalToWorldPosition(shape2D.Vertices[i].Point * currentEdgeRingScale));
                    normals.Add(orientedPoint.LocalToWorldVector(shape2D.Vertices[i].Normal));
                }
            }

            // Triangles
            var triangleIndices = new List<int>();

            for (var ring = 0; ring < edgeRingCount - 1; ring++)
            {
                var rootIndex = ring * shape2D.VertexCount;
                var rootIndexNext = (ring + 1) * shape2D.VertexCount;

                // line means on line on the 2D shape
                for (var line = 0; line < shape2D.LineCount; line += 2)
                {
                    var lineIndexA = shape2D.LineIndices[line];
                    var lineIndexB = shape2D.LineIndices[line + 1];

                    var currentA = rootIndex + lineIndexA;
                    var currentB = rootIndex + lineIndexB;
                    var nextA = rootIndexNext + lineIndexA;
                    var nextB = rootIndexNext + lineIndexB;

                    // Every Quad of the mesh that we are generating consists of two triangles connecting vertices from one edgeRing to the next one.

                    // Triangle 1
                    triangleIndices.Add(currentA);
                    triangleIndices.Add(nextA);
                    triangleIndices.Add(nextB);

                    // Triangle 2
                    triangleIndices.Add(currentA);
                    triangleIndices.Add(nextB);
                    triangleIndices.Add(currentB);
                }
            }

            mesh.SetVertices(verts);
            mesh.SetNormals(normals);
            mesh.SetTriangles(triangleIndices, 0);
        }

        private void OnDrawGizmos()
        {
#if UNITY_EDITOR
            var startPosition = bezierStart.position;
            var endPosition = bezierEnd.position;
            var startTangent = bezierStartTangent.position;
            var endTangent = bezierEndTangent.position;

            Gizmos.DrawSphere(startPosition, controlPointRadius);
            Gizmos.DrawSphere(startTangent, controlPointRadius);
            Gizmos.DrawSphere(endPosition, controlPointRadius);
            Gizmos.DrawSphere(endTangent, controlPointRadius);

            Handles.DrawBezier(startPosition, endPosition, startTangent, endTangent, Color.white,
                EditorGUIUtility.whiteTexture, 1f);

            var testPoint = GetBezierOrientedPoint(tTest);
            Handles.PositionHandle(testPoint.Position, testPoint.Rotation);

            // Debug - draw points along bezier curve 
            // void DrawPoint(Vector3 localPos) => Gizmos.DrawSphere(testPoint.LocalToWorldPosition(localPos), controlPointRadius);
#endif
        }

        private OrientedPoint GetBezierOrientedPoint(float t)
        {
            var p0 = bezierStart.position;
            var p1 = bezierStartTangent.position;
            var p2 = bezierEndTangent.position;
            var p3 = bezierEnd.position;

            var a = Vector3.Lerp(p0, p1, t);
            var b = Vector3.Lerp(p1, p2, t);
            var c = Vector3.Lerp(p2, p3, t);

            var d = Vector3.Lerp(a, b, t);
            var e = Vector3.Lerp(b, c, t);

            var pos = Vector3.Lerp(d, e, t);
            var tangent = (e - d).normalized;

            return new OrientedPoint(pos, tangent);
        }
    }
}