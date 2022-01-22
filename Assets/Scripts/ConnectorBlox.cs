using System;
using System.Collections;
using System.Collections.Generic;
using blox.procedural;
using UnityEngine;

namespace blox
{
    public class ConnectorBlox : Blox
    {
        [SerializeField] private CubicBezierPart ConnectionBezierEnd;

        private List<Blox> connectedBlox = new List<Blox>();

        protected override void OnTriggerEnter(Collider other)
        {
            var otherBlox = other.GetComponent<Blox>();
            if (otherBlox)
            {
                ConnectBlox(otherBlox);
            }

            base.OnTriggerEnter(other);
        }

        // protected override void OnTriggerExit(Collider other)
        // {
        //     // var generatorZone = other.GetComponent<GeneratorZone>();
        //     // if (generatorZone)
        //     // {
        //     //     DisconnectAllBlox();
        //     // }
        //     
        //     base.OnTriggerExit(other);
        // }
        
        public void ConnectBlox(Blox blox)
        {
            blox.SetBezierEnd(ConnectionBezierEnd);
            blox.SetBezierStartToConnector();
            connectedBlox.Add(blox);
        }
        
        public void DisconnectBlox(Blox blox)
        {
            var root = transform.parent.GetComponent<Root>();
            if (root)
            {
                blox.SetBezierEnd(root.CubicBezier.End);
                blox.SetBezierStartToRoot();
            }

            connectedBlox.Remove(blox);
        }
        
        public void DisconnectAllBlox()
        {
            var root = transform.parent.GetComponent<Root>();

            foreach (var blox in connectedBlox)
            {
                blox.SetBezierEnd(root.CubicBezier.End);
                blox.SetBezierStartToRoot();
            }
            
            connectedBlox.Clear();
        }
    }
}