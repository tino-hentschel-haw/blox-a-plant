using System.Collections.Generic;
using blox.procedural;
using UnityEngine;

namespace blox
{
    public class Root : MonoBehaviour
    {
        [SerializeField] private CubicBezier cubicBezier;
        public CubicBezier CubicBezier => cubicBezier;

        private List<Blox> allBlox = new List<Blox>();

        public void AddBlox(Blox blox)
        {
            if(!allBlox.Contains(blox))
                allBlox.Add(blox);
        }
        
        public void RemoveBlox(Blox blox)
        {
            allBlox.Remove(blox);
        }
    }
}