using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(MeshFilter))]
public class Segment : MonoBehaviour
{
    [Range(0, 1)] [SerializeField] private float tTest = 0;

    [SerializeField] private Transform[] controlPoints = new Transform[4];

    private Vector3 GetPos(int i) => controlPoints[i].position;

    private void OnDrawGizmos()
    {
        for (var i = 0; i < 4; i++)
        {
            Gizmos.DrawSphere(GetPos(i), 0.05f);
        }

        Handles.DrawBezier(GetPos(0), GetPos(3), GetPos(1), GetPos(2), Color.white, EditorGUIUtility.whiteTexture, 1f);

        
        var testPoint = GetBezierOrientedPoint(tTest);
        var radius = 0.01f;
        void DrawPoint(Vector3 localPos) => Gizmos.DrawSphere(testPoint.LocalToWorld(localPos), radius);
        

        Handles.PositionHandle(testPoint.Position, testPoint.Rotation);
        
        Gizmos.color = Color.red;
        
        DrawPoint(Vector3.right * 0.1f);
        DrawPoint(Vector3.right * 0.2f);
        DrawPoint(Vector3.right * -0.1f);
        DrawPoint(Vector3.right * -0.2f);
        
        DrawPoint(Vector3.up * 0.1f);
        DrawPoint(Vector3.up * 0.2f);

        Gizmos.color = Color.white;
    }

    private OrientedPoint GetBezierOrientedPoint(float t)
    {
        var p0 = GetPos(0);
        var p1 = GetPos(1);
        var p2 = GetPos(2);
        var p3 = GetPos(3);

        var a = Vector3.Lerp(p0, p1, t);
        var b = Vector3.Lerp(p1, p2, t);
        var c = Vector3.Lerp(p2, p3, t);

        var d = Vector3.Lerp(a, b, t);
        var e = Vector3.Lerp(b, c, t);

        var pos = Vector3.Lerp(d, e, t);
        var tangent = (e-d).normalized;

        return new OrientedPoint(pos, tangent);
    }
}