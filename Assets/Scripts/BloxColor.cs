using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloxColor : MonoBehaviour
{
    [SerializeField] private GameObject BloxGO;
    [SerializeField] private GameObject PlantGO;


    public void SetColor(Color color)
    {
        BloxGO.GetComponent<MeshRenderer>().material.color = color;
        PlantGO.GetComponent<MeshRenderer>().material.color = color;
    }
}
