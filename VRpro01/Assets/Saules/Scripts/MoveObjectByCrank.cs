using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class MoveObjectByCrank : MonoBehaviour
{
    public Transform objectToRotate; // the cylinder to rotate
    public float degreesPerStep = 20f; // how many degrees to rotate the cylinder per step
    public float rotationSpeed = 5f;
    public float maxRotationSpeed = 10f;
    public float rotationAcceleration = 2f;
    public float rotationDeceleration = 4f;

    private float previousRotation = 0f;
    private bool isGrabbed = false;
    private float currentRotationSpeed = 0f;
    private XRGrabInteractable grabInteractable;

    private void Start()
    {
        // get a reference to the XRGrabInteractable component
        grabInteractable = GetComponent<XRGrabInteractable>();

        // register event handlers for grab and release events
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void Update()
    {
        if (isGrabbed)
        {
            // get the current rotation of the handwheel
            float currentRotation = transform.rotation.eulerAngles.y;

            // calculate the change in rotation since the last frame
            float rotationDelta = currentRotation - previousRotation;

            // calculate the number of steps to rotate the object based on the rotation delta
            int steps = Mathf.RoundToInt(rotationDelta / degreesPerStep);

            // calculate the target rotation of the object
            Quaternion targetRotation = Quaternion.Euler(0f, steps * degreesPerStep, 0f) * objectToRotate.rotation;

            // gradually increase the rotation speed
            currentRotationSpeed += rotationAcceleration * Time.deltaTime;

            // clamp the rotation speed to the maximum speed
            currentRotationSpeed = Mathf.Clamp(currentRotationSpeed, 0f, maxRotationSpeed);

            // smoothly rotate the object towards the target rotation with the current rotation speed
            objectToRotate.rotation = Quaternion.Slerp(objectToRotate.rotation, targetRotation, currentRotationSpeed * Time.deltaTime);

            // gradually decrease the rotation speed if the object is close to the target rotation
            if (Quaternion.Angle(objectToRotate.rotation, targetRotation) < 1f)
            {
                currentRotationSpeed -= rotationDeceleration * Time.deltaTime;

                // clamp the rotation speed to 0 if it becomes negative
                currentRotationSpeed = Mathf.Max(currentRotationSpeed, 0f);
            }

            // store the current rotation for the next frame
            previousRotation = currentRotation;
        }
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        isGrabbed = true;
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        isGrabbed = false;
        currentRotationSpeed = 0f; // reset the rotation speed when the object is released
    }
}