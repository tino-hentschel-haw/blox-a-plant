using blox.procedural;
using UnityEngine;


namespace blox
{
    /// <summary>
    /// TODO This is demo code! Refactor or remove this hacky mess ...
    /// </summary>
    public class Blox : MonoBehaviour
    {
        [SerializeField] private GameObject bloxGameObject;
        [SerializeField] private GameObject plantGameObject;
        
        [SerializeField] private CubicBezierPart bezierStart;
        
        [SerializeField] private BezierMeshGenerator bezierMeshGeneratorPrefab;
        private BezierMeshGenerator bezierMeshGenerator;
        
        private Rigidbody rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            bezierMeshGenerator = Instantiate(bezierMeshGeneratorPrefab, Vector3.zero, Quaternion.identity);
            bezierMeshGenerator.SetBezierStart(bezierStart);
            bezierMeshGenerator.gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            var generatorZone = other.GetComponent<GeneratorZone>();
            
            if (!generatorZone)
                return;

            var root = generatorZone.Root;
            transform.SetParent(root.transform);
            
            bezierMeshGenerator.SetBezierEnd(root.CubicBezier.End);
            bezierMeshGenerator.gameObject.SetActive(true);
           
            bloxGameObject.SetActive(false);
            plantGameObject.SetActive(true);
            
            rb.isKinematic = true;
        }

        private void OnTriggerExit(Collider other)
        {
            var generatorZone = other.GetComponent<GeneratorZone>();
            if (!generatorZone)
                return;

            bloxGameObject.SetActive(true);
            plantGameObject.SetActive(false);
            bezierMeshGenerator.gameObject.SetActive(false);
            rb.isKinematic = false;
        }

        public void TryDisableKinematic()
        {
            if (bloxGameObject.activeInHierarchy)
                rb.isKinematic = false;
        }
    }
}