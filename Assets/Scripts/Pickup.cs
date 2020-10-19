using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    // Initialize variables
    [SerializeField] Camera firstPersonCamera = null;
    [SerializeField] float grabRange = 2f;
    [SerializeField] Transform destination = null;
    private RaycastHit hitInfo;
    private bool canGrab;

    // Start and end markers for journey 
    private Vector3 endPosition;
    private Vector3 startPosition;

    // Changing lerp time will increase or decrease travel time
    [SerializeField] float lerpTime = 0.2f;

    // Time when movement started
    private float timeStartedLerping;

    // This float keeps track of if the lerp is complete
    private float percentageComplete;

    // Update is called once per frame
    void Update()
    {
        GoLerp();

        if (percentageComplete >= 1f)
        {
            Grab();
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (canGrab && hitInfo.transform != null)
            {
                Debug.Log("Set down object");
                hitInfo.transform.GetComponent<Rigidbody>().useGravity = true;
                hitInfo.transform.GetComponent<Rigidbody>().isKinematic = false;
                hitInfo.transform.GetComponent<BoxCollider>().isTrigger = false;
            }
            if (Physics.Raycast(firstPersonCamera.transform.position, firstPersonCamera.transform.forward, out hitInfo, grabRange) && hitInfo.transform.tag == "Moveable")
            {
                Debug.Log("The object name hit is: " + hitInfo.transform.name + " The tag is: " + hitInfo.transform.tag);
                hitInfo.transform.GetComponent<Rigidbody>().useGravity = false;
                hitInfo.transform.GetComponent<Rigidbody>().isKinematic = true;
                hitInfo.transform.GetComponent<BoxCollider>().isTrigger = true;
                canGrab = !canGrab;

                timeStartedLerping = Time.time;
                startPosition = hitInfo.transform.position;
                endPosition = destination.transform.position;
            }
        }
    }

    // Allows for smooth movement to first-person hands
    void GoLerp()
    {
        if (canGrab && hitInfo.transform != null)
        {
            hitInfo.transform.position = Lerp(startPosition, endPosition, timeStartedLerping, lerpTime);
        }
    }

    // Snaps and rotates to hand
    void Grab()
    {
        if (canGrab && hitInfo.transform != null)
        {
            hitInfo.transform.position = destination.transform.position;
            hitInfo.transform.rotation = destination.transform.rotation;
        }
    }

    public Vector3 Lerp(Vector3 start, Vector3 end, float timeStartedLerping, float lerpTime = 0.1f)
    {
        float timeSinceStarted = Time.time - timeStartedLerping;
        percentageComplete = timeSinceStarted / lerpTime;
        var result = Vector3.Lerp(start, end, percentageComplete);

        return result;
    }
}
