using UnityEngine;

namespace blox.procedural
{
    [System.Serializable]
    public struct CubicBezier
    {
        public CubicBezierPart Start;
        public CubicBezierPart End;
    }
    
    [System.Serializable]
    public struct CubicBezierPart
    {
        public Transform Point;
        public Transform Tangent;
    }
}