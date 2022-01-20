using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloxSpawner : MonoBehaviour
{
    [SerializeField] private GameObject blox;
    private Vector3 bloxSpawnerPos;
    private bool isSpawnerFree = true;
    private GameObject lastSpawnedBlox;

    void Start()
    {
        //get position
        bloxSpawnerPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
       //spawn new blox
       spawnBlox();

    }

    public void spawnBlox(){
        //check if spawner is free
        if (isSpawnerFree){
            GameObject newBlox = Instantiate(blox) as GameObject;
            newBlox.transform.position = bloxSpawnerPos;
            lastSpawnedBlox = newBlox;
            isSpawnerFree = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        isSpawnerFree = true;
    }

}
