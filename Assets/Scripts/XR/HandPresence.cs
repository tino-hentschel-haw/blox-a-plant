using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{
    public bool showController;
    public InputDeviceCharacteristics ControllerCharacteristics;
    public List<GameObject> ControllerPrefabs;
    public GameObject HandModelPrefab;

    private InputDevice targetDevice;
    private GameObject spawnedController;
    private GameObject spawnedHandModel;
    private Animator handAnimator;

    void Start()
    {
        TryInitialize();
    }

    void Update()
    {
        if (!targetDevice.isValid)
        {
            TryInitialize();
        }
        else
        {
            if (showController)
            {
                spawnedHandModel.SetActive(false);
                spawnedController.SetActive(true);
            }
            else
            {
                spawnedHandModel.SetActive(true);
                spawnedController.SetActive(false);
                UpdateHandAnimation();
            }
        }
    }

    private void TryInitialize()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(ControllerCharacteristics, devices);

        if (devices.Count > 0)
        {
            targetDevice = devices[0];
            GameObject controllerPrefab = ControllerPrefabs.Find(controller => controller.name == targetDevice.name);
            
            if (controllerPrefab)
            {
                spawnedController = Instantiate(controllerPrefab, transform);
            }
            else
            {
                Debug.LogWarning("Did not find corresponding controller. Using default controller model");
                spawnedController = Instantiate(ControllerPrefabs[0], transform);
            }
            
            spawnedHandModel = Instantiate(HandModelPrefab, transform);
            handAnimator = spawnedHandModel.GetComponent<Animator>();
        }
    }

    private void UpdateHandAnimation()
    {
        handAnimator.SetFloat("Trigger", 
            targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue) ? triggerValue : 0.0f);
        
        handAnimator.SetFloat("Grip",
            targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue) ? gripValue : 0.0f);
    }
}