using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneTrigger : MonoBehaviour
{
    public GameObject objectToSpawn;
    public Transform spawnLocation;
    public GameObject button;   

    private bool isButtonPressed = false;
    private bool isMoverInZone;

    public float buttonPressedTime = 2.0f; // adjust the duration as needed

    public void ButtonPressed()
    {
        isButtonPressed = true;
        StartCoroutine(ResetButtonPressed());
    }

    private IEnumerator ResetButtonPressed()
    {
        yield return new WaitForSeconds(buttonPressedTime);
        isButtonPressed = false;
    }

    private void Update()
    {
        if (isButtonPressed && isMoverInZone)
        {
            Instantiate(objectToSpawn, spawnLocation.position, spawnLocation.rotation);
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mover"))
        {
            isMoverInZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Mover"))
        {
            isMoverInZone = false;
        }
    }
}