using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MoveObjectByLever : MonoBehaviour
{
    public XRGrabInteractable lever;
    public Transform cylinder;
    public float maxSpeed = 2.0f;
    public float velocityOffset = 0.2f;

    private float startingAngle = Quaternion.Euler(0, 0, 0).eulerAngles.z;
    private bool isGrabbed = false;
    private XRGrabInteractable grabInteractable;
    private Vector3 topSurfaceNormal;

    private void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);

        // Calculate the normal vector of the top surface of the cylinder
        topSurfaceNormal = cylinder.TransformDirection(Vector3.up);
        Collider cylinderCollider = cylinder.GetComponent<Collider>();
        Vector3 topSurfacePoint = cylinderCollider.ClosestPoint(cylinder.transform.position + cylinder.TransformDirection(Vector3.up) * cylinder.localScale.y / 2f);
        topSurfaceNormal = (topSurfacePoint - cylinder.transform.position).normalized;
    }

   private void Update()
{
    // Only move the cylinder if the lever is grabbed
    if (isGrabbed)
    {
        // Calculate the current angle of the lever relative to the starting angle
        float currentAngle = lever.transform.localRotation.eulerAngles.z;
        Debug.Log("Current angle: " + currentAngle);

        if (currentAngle >= 10 && currentAngle <= 60)
        {
            // Calculate the velocity based on the current angle
            float velocity = (currentAngle - startingAngle) * (maxSpeed / 60f) + velocityOffset;
            Debug.Log("Velocity+: " + velocity);

            // Calculate the direction to move the cylinder
            Vector3 direction = cylinder.up;
            Debug.DrawLine(cylinder.position, cylinder.position + direction, Color.blue);

            // Move the cylinder
            Vector3 newPosition = cylinder.transform.position;
            newPosition += direction * velocity * Time.deltaTime;
            cylinder.transform.position = newPosition;
        }
        else if (currentAngle >= 300 && currentAngle <= 350)
        {
            // Calculate the velocity based on the current angle
            float velocity = (360 - currentAngle) * (-maxSpeed / 60f) + velocityOffset;
            Debug.Log("Velocity-: " + velocity);

            // Calculate the direction to move the cylinder
            Vector3 direction = cylinder.up;
            Debug.DrawLine(cylinder.position, cylinder.position + direction, Color.blue);

            // Move the cylinder
            Vector3 newPosition = cylinder.transform.position;
            newPosition += direction * velocity * Time.deltaTime;
            cylinder.transform.position = newPosition;
        }
    }
}

    private void OnGrab(SelectEnterEventArgs args)
    {
        isGrabbed = true;
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        isGrabbed = false;
        ResetLever();
    }

    private void ResetLever()
    {
        // Reset the lever to its original starting position
        lever.transform.localRotation = Quaternion.Euler(0, 0, startingAngle);
    }
}