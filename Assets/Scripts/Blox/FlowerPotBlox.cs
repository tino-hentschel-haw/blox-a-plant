using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace blox
{
    public class FlowerPotBlox : MonoBehaviour
    {
        [SerializeField] protected GameObject flowerpotGameObject;
        [SerializeField] protected GameObject flowerpotEmptyGameObject;
        [SerializeField] protected Transform plantAttach;
        [SerializeField] protected GameObject plantTriggerZone;
        [SerializeField] protected Transform plantPoint;

        [Header("Audio")] [SerializeField] private AudioClip plantSound;
        [SerializeField] private AudioClip transitionSound;
        private AudioSource audioSrc;

        private GameObject plantGameObject;
        public bool HasPlant { get; private set; }

        private bool selected;

        public bool Selected
        {
            get => selected;
            protected set
            {
                if (plantGameObject)
                    plantGameObject.SetActive(value);

                if (plantTriggerZone)
                    plantTriggerZone.SetActive(value);

                selected = value;
            }
        }

        private XRGrabInteractable xrGrabInteractable;

        private void Awake()
        {
            xrGrabInteractable = GetComponent<XRGrabInteractable>();
            audioSrc = GetComponent<AudioSource>();
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

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Ground") && Selected && HasPlant)
            {
                Instantiate(plantGameObject, plantPoint.position, Quaternion.identity);
                audioSrc.PlayOneShot(plantSound);
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

            flowerpotGameObject.SetActive(true);
            flowerpotEmptyGameObject.SetActive(false);

            root.ClearAllBlox();
            
            audioSrc.PlayOneShot(transitionSound);
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