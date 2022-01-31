using UnityEngine;

namespace blox
{
    [RequireComponent(typeof(Blox))]
    public class ColorableBlox : MonoBehaviour
    {
        private MeshRenderer bloxMeshRenderer;
        private MeshRenderer plantMeshRenderer;

        protected AudioSource[] audios;

        private void Awake()
        {
            var blox = GetComponent<Blox>();
            bloxMeshRenderer = blox.BloxGameObject.GetComponent<MeshRenderer>();
            plantMeshRenderer = blox.PlantGameObject.GetComponent<MeshRenderer>();
            audios = GetComponents<AudioSource>();
        }

        public void SetColor(Color color)
        {
            bloxMeshRenderer.material.color = color;
            plantMeshRenderer.material.color = color;
            
            //Coloring Sound
            audios[2].Play();
        }
    }
}