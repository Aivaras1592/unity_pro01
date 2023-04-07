using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOverlapDisable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Spawner"))
        {
          other.gameObject.SetActive(false);
        }
    }
}
