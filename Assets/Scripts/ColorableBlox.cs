using UnityEngine;

namespace blox
{
    [RequireComponent(typeof(Blox))]
    public class ColorableBlox : MonoBehaviour
    {
        private MeshRenderer bloxMeshRenderer;
        private MeshRenderer plantMeshRenderer;

        private void Awake()
        {
            var blox = GetComponent<Blox>();
            bloxMeshRenderer = blox.BloxGameObject.GetComponent<MeshRenderer>();
            plantMeshRenderer = blox.PlantGameObject.GetComponent<MeshRenderer>();
        }

        public void SetColor(Color color)
        {
            bloxMeshRenderer.material.color = color;
            plantMeshRenderer.material.color = color;
            
            // TODO Play Sound when changing color here ...
        }
    }
}