using blox.procedural;
using UnityEngine;
using UnityEngine.Serialization;


namespace blox
{
    public class Blox : MonoBehaviour
    {
        [SerializeField] protected GameObject bloxGameObject;
        [SerializeField] protected GameObject plantGameObject;
        public GameObject BloxGameObject => bloxGameObject;
        public GameObject PlantGameObject => plantGameObject;
        
        [FormerlySerializedAs("bezierStart")] [SerializeField]
        protected CubicBezierPart bezierStartRoot;

        [SerializeField] protected CubicBezierPart bezierStartConnector;

        [SerializeField] private BezierMeshGenerator bezierMeshGeneratorPrefab;
        protected BezierMeshGenerator bezierMeshGenerator;

        public BezierMeshGenerator BezierMeshGenerator => bezierMeshGenerator;

        protected Rigidbody rb;
        protected BoxCollider boxCollider;

        public bool Selected { get; protected set; }
        public bool InGeneratorZone { get; protected set; }
        public bool ConnectedToRoot { get; protected set; }

        
        [Header("Audio")] [SerializeField] protected AudioClip addBloxSound;
        [SerializeField] private AudioClip transitionSound;
        protected AudioSource audioSrc;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            boxCollider = GetComponent<BoxCollider>();
            bezierMeshGenerator = Instantiate(bezierMeshGeneratorPrefab, Vector3.zero, Quaternion.identity);
            SetBezierStartToRoot();
            bezierMeshGenerator.gameObject.SetActive(false);
            audioSrc = GetComponent<AudioSource>();
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            var generatorZone = other.GetComponent<GeneratorZone>();
            if (!generatorZone)
                return;

            if (InGeneratorZone)
                return;

            InGeneratorZone = true;

            //Entering Sound
            audioSrc.PlayOneShot(transitionSound);

            var root = generatorZone.Root;
            root.AddBlox(this);
            transform.SetParent(root.transform);

            bezierMeshGenerator.SetBezierEnd(root.CubicBezier.End);
            bezierMeshGenerator.gameObject.SetActive(true);

            bloxGameObject.SetActive(false);
            plantGameObject.SetActive(true);

            rb.isKinematic = true;
            boxCollider.isTrigger = true;
        }

        protected virtual void OnTriggerExit(Collider other)
        {
            var generatorZone = other.GetComponent<GeneratorZone>();
            if (!generatorZone)
                return;

            if (!Selected)
                return;

            //Exit Sound
            audioSrc.PlayOneShot(transitionSound);

            InGeneratorZone = false;

            var root = generatorZone.Root;
            root.RemoveBlox(this);
            transform.SetParent(null);

            bloxGameObject.SetActive(true);
            plantGameObject.SetActive(false);
            bezierMeshGenerator.gameObject.SetActive(false);
            //rb.isKinematic = false;
            boxCollider.isTrigger = false;
        }

        public void TryDisableKinematic()
        {
            rb.isKinematic = InGeneratorZone;
        }

        public void SetSelected(bool isSelected)
        {
            Selected = isSelected;
        }

        public void SetBezierStartToRoot()
        {
            bezierMeshGenerator.SetBezierStart(bezierStartRoot);
            ConnectedToRoot = true;
        }

        public void SetBezierStartToConnector()
        {
            bezierMeshGenerator.SetBezierStart(bezierStartConnector);
            ConnectedToRoot = false;
        }

        public void SetBezierEnd(CubicBezierPart bezierEndPart)
        {
            bezierMeshGenerator.SetBezierEnd(bezierEndPart);
        }
    }
}