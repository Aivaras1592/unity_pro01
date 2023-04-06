using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectOnTrigger : MonoBehaviour
{
    public GameObject objectToSpawn; // the prefab of the object to spawn
    public Transform spawnLocation; // the location to spawn the object at

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Zone")
        {
            Instantiate(objectToSpawn, spawnLocation.position, spawnLocation.rotation);
            other.gameObject.SetActive(false); // disable the whole thing
        }
    }
}