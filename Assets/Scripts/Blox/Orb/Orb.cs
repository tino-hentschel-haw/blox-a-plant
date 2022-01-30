using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace blox.orb
{
    [RequireComponent(typeof(XRGrabInteractable))]
    public class Orb : MonoBehaviour
    {
        public bool Selected { get; protected set; }

        private XRGrabInteractable xrGrabInteractable;
        private MeshRenderer meshRenderer;

        private void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            xrGrabInteractable = GetComponent<XRGrabInteractable>();
        }

        private void OnEnable()
        {
            xrGrabInteractable.selectEntered.AddListener(OnSelectEntered);
            xrGrabInteractable.selectExited.AddListener(OnSelectExited);
        }

        private void OnDisable()
        {
            xrGrabInteractable.selectEntered.RemoveListener(OnSelectEntered);
            xrGrabInteractable.selectExited.RemoveListener(OnSelectExited);
        }

        private void OnTriggerEnter(Collider col)
        {
            var colorableBlox = col.gameObject.GetComponent<ColorableBlox>();
            if (!colorableBlox)
                return;
            
            colorableBlox.SetColor(meshRenderer.material.color);
        }

        private void OnCollisionEnter(Collision other)
        {
            if(other.collider.gameObject.layer == LayerMask.NameToLayer("Ground") && !Selected)
            {
                gameObject.SetActive(false);
            }
        }

        public void SetColor(Color color)
        {
            meshRenderer.material.color = color;
        }

        private void OnSelectEntered(SelectEnterEventArgs eventArgs)
        {
            if (eventArgs.interactorObject is XRDirectInteractor)
            {
                Selected = true;
            }
                
        }

        private void OnSelectExited(SelectExitEventArgs eventArgs)
        {
            if (eventArgs.interactorObject is XRDirectInteractor)
            {
                Selected = false;
            }
        }
    }
}