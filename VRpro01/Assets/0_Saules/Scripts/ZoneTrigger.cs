using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneTrigger : MonoBehaviour
{
    public GameObject objectToSpawn;
    public Transform spawnLocation;


    private void OnTriggerEnter(Collider other)
    {
        if ( other.gameObject.layer == LayerMask.NameToLayer("Icon"))
        {
            Instantiate(objectToSpawn, spawnLocation.position, spawnLocation.rotation);
      
            gameObject.SetActive(false);
        }
    }
}