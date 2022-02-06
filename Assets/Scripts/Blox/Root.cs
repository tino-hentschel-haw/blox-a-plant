using System.Collections.Generic;
using blox.procedural;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace blox
{
    public class Root : MonoBehaviour
    {
        [SerializeField] private GameObject plantBasePrefab;
        [SerializeField] private CubicBezier cubicBezier;
        public CubicBezier CubicBezier => cubicBezier;

        private List<Blox> allBlox = new List<Blox>();

        public bool HasBlox() => allBlox.Count > 0;
        
        public void AddBlox(Blox blox)
        {
            if(!allBlox.Contains(blox))
                allBlox.Add(blox);
        }
        
        public void RemoveBlox(Blox blox)
        {
            allBlox.Remove(blox);
        }

        public GameObject GetPlantGameObject()
        {
            var plantGameObject = new GameObject("Plant");

            foreach (var blox in allBlox)
            {
                blox.GetComponent<XRGrabInteractable>().enabled = false;
                blox.transform.SetParent(plantGameObject.transform);
                blox.BezierMeshGenerator.Generate = false;
                blox.BezierMeshGenerator.enabled = false;
                blox.GetComponent<AudioSource>().enabled = false;

                // var meshGameObject = new GameObject("Generated Mesh from " + blox.BezierMeshGenerator.gameObject.name);
                // meshGameObject.AddComponent<MeshFilter>();
                // meshGameObject.AddComponent<MeshRenderer>();
                //
                // meshGameObject.GetComponent<MeshFilter>().mesh =
                //     blox.BezierMeshGenerator.GetComponent<MeshFilter>().mesh;
                // meshGameObject.GetComponent<MeshRenderer>().material = 
                
                blox.BezierMeshGenerator.transform.SetParent(plantGameObject.transform);
            }

            var plantBase = Instantiate(plantBasePrefab);
            plantBase.transform.SetPositionAndRotation(plantBasePrefab.transform.position, plantBasePrefab.transform.rotation);
            plantBase.transform.SetParent(plantGameObject.transform);
            
            return plantGameObject;
        }

        public void ClearAllBlox()
        {
            allBlox.Clear();
        }
    }
}