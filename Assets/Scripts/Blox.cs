using UnityEngine;

/// <summary>
/// TODO This is demo code! Refactor or remove this hacky mess ...
/// </summary>
public class Blox : MonoBehaviour
{
   [SerializeField] private GameObject bloxGameObject;
   [SerializeField] private GameObject plantGameObject;
   [SerializeField] private GameObject plantBezierMeshGameObject;

   private Rigidbody rb;
   
   private void Awake()
   {
      rb = GetComponent<Rigidbody>();
   }

   private void OnTriggerEnter(Collider other)
   {
      if (!other.CompareTag("plantZone")) 
         return;
      
      bloxGameObject.SetActive(false);
      plantGameObject.SetActive(true);
      plantBezierMeshGameObject.SetActive(true);
      rb.isKinematic = true;
   }
   
   private void OnTriggerExit(Collider other)
   {
      if (!other.CompareTag("plantZone")) 
         return;
      
      bloxGameObject.SetActive(true);
      plantGameObject.SetActive(false);
      plantBezierMeshGameObject.SetActive(false);
      rb.isKinematic = false;
   }

   public void TryDisableKinematic()
   {
      if (bloxGameObject.activeInHierarchy)
         rb.isKinematic = false;
   }
}
