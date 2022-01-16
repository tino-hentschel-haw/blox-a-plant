using System;
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
        for (int i = 0; i < 4; i++)
        {
            Gizmos.DrawSphere(GetPos(i), 0.05f);
        }

        Handles.DrawBezier(GetPos(0), GetPos(3), GetPos(1), GetPos(2), Color.white, EditorGUIUtility.whiteTexture, 1f);

        var testPoint = GetBezierPoint(tTest);
        
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(testPoint, 0.05f);
        Gizmos.color = Color.white;
    }

    private Vector3 GetBezierPoint(float t)
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

        return Vector3.Lerp(d, e, t);
    }
}