using UnityEngine;

namespace blox
{
    [RequireComponent(typeof(Blox))]
    public class ColorableBlox : MonoBehaviour
    {
        private MeshRenderer bloxMeshRenderer;
        private MeshRenderer plantMeshRenderer;

        [SerializeField] private AudioClip coloringSound;
        private AudioSource audioSrc;

        private void Awake()
        {
            var blox = GetComponent<Blox>();
            bloxMeshRenderer = blox.BloxGameObject.GetComponent<MeshRenderer>();
            plantMeshRenderer = blox.PlantGameObject.GetComponent<MeshRenderer>();
            audioSrc = GetComponent<AudioSource>();
        }

        public void SetColor(Color color)
        {
            bloxMeshRenderer.material.color = color;
            plantMeshRenderer.material.color = color;

            //Coloring Sound
            audioSrc.PlayOneShot(coloringSound);
        }
    }
}