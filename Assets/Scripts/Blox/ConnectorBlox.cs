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
                if (otherBlox is ConnectorBlox)
                {
                    var otherConnector = other.GetComponent<ConnectorBlox>();
                    if (!otherConnector.ContainsBlox(this))
                    {
                        ConnectBlox(otherBlox);
                    }
                    
                    // if(otherBlox.Selected )
                    //     ConnectBlox(otherBlox);
                }
                else
                {
                    ConnectBlox(otherBlox);
                }
            }

            base.OnTriggerEnter(other);
        }

        protected override void OnTriggerExit(Collider other)
        {
            var generatorZone = other.GetComponent<GeneratorZone>();
            if (generatorZone)
            {
                DisconnectAllBlox(generatorZone.Root);
            }
            
            base.OnTriggerExit(other);
        }
        
        public void ConnectBlox(Blox blox)
        {
            blox.SetBezierEnd(ConnectionBezierEnd);
            blox.SetBezierStartToConnector();
            connectedBlox.Add(blox);
            
            //Connecting Sound
            audioSrc.PlayOneShot(addBloxSound);
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
        
        public void DisconnectAllBlox(Root root)
        {
            foreach (var blox in connectedBlox)
            {
                blox.SetBezierEnd(root.CubicBezier.End);
                blox.SetBezierStartToRoot();
            }
            
            connectedBlox.Clear();
        }

        public bool ContainsBlox(Blox blox)
        {
            return connectedBlox.Contains(blox);
        }
    }
}