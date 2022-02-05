using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace blox
{
    public class FlowerPotBlox : MonoBehaviour
    {
        [SerializeField] protected GameObject bloxGameObject;
        [SerializeField] protected Transform plantAttach;
        [SerializeField] protected GameObject plantTriggerZone;
        [SerializeField] protected Transform plantPoint;
        
        private GameObject plantGameObject;
        public bool HasPlant { get; private set; }

        private bool selected;

        public bool Selected
        {
            get => selected;
            protected set
            {
                if(plantGameObject)
                    plantGameObject.SetActive(value);
                
                if(plantTriggerZone)
                    plantTriggerZone.SetActive(value);
                
                selected = value;
            }
        }

        private XRGrabInteractable xrGrabInteractable;

        private void Awake()
        {
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

        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.gameObject.layer == LayerMask.NameToLayer("Ground") && Selected && HasPlant)
            {
                Debug.Log("Ground Col: " + other.contacts[0].point);

                Instantiate(plantGameObject, other.contacts[0].point, Quaternion.identity);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Ground") && Selected && HasPlant)
            {
                Instantiate(plantGameObject, plantPoint.position, Quaternion.identity);
            }
            
            if (!Selected)
                return;

            var generatorZone = other.GetComponent<GeneratorZone>();
            if (!generatorZone)
                return;

            SetPlantGameObject(generatorZone.Root);
        }

        private void SetPlantGameObject(Root root)
        {
            if (!root.HasBlox())
                return;

            plantGameObject = root.GetPlantGameObject();

            plantGameObject.transform.SetPositionAndRotation(plantAttach.position, plantAttach.rotation);
            plantGameObject.transform.localScale = plantAttach.localScale;
            plantGameObject.transform.SetParent(plantAttach);

            HasPlant = true;

            root.ClearAllBlox();
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