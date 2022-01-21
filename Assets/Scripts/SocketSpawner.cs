using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRSocketInteractor))]
public class SocketSpawner : MonoBehaviour
{
    [SerializeField] private XRGrabInteractable grabInteractablePrefab;

    private XRSocketInteractor socketInteractor;
    private XRGrabInteractable currentGrabInteractable;

    private void Awake()
    {
        socketInteractor = GetComponent<XRSocketInteractor>();
        Application.quitting += () => socketInteractor.selectExited.RemoveListener(OnSelectExited);
    }

    private void Start() => TrySpawn();

    private void OnEnable() => socketInteractor.selectExited.AddListener(OnSelectExited);

    private void OnDisable() => socketInteractor.selectExited.RemoveListener(OnSelectExited);

    private void OnSelectExited(SelectExitEventArgs eventArgs) => TrySpawn();

    private void TrySpawn()
    {
        if (socketInteractor.hasSelection || !grabInteractablePrefab)
            return;

        var interactable = Instantiate(grabInteractablePrefab);
        interactable.transform.position = socketInteractor.attachTransform.position;
        interactable.transform.rotation = socketInteractor.attachTransform.rotation;
    }
}