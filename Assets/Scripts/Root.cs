using blox.procedural;
using UnityEngine;

namespace blox
{
    public class Root : MonoBehaviour
    {
        [SerializeField] private CubicBezier cubicBezier;
        public CubicBezier CubicBezier => cubicBezier;
    }
}