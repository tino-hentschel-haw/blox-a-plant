using UnityEngine;

namespace blox
{
    public class Orb : MonoBehaviour
    {
        public bool Selected { get; protected set; }
        
        private MeshRenderer meshRenderer;

        private void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }

        private void OnTriggerEnter(Collider col)
        {
            var colorableBlox = col.gameObject.GetComponent<ColorableBlox>();
            if (!colorableBlox)
                return;
            
            colorableBlox.SetColor(meshRenderer.material.color);
        }

        public void SetColor(Color color)
        {
            meshRenderer.material.color = color;
        }
        
        public void SetSelected(bool isSelected)
        {
            Selected = isSelected;
        }
    }
}