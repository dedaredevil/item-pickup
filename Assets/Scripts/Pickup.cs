using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    // Initialize variables
    [SerializeField] Camera firstPersonCamera = null;
    [SerializeField] float range = 2f;
    [SerializeField] Vector3 rayOffset = new Vector3(0,0,0.3f);
    [SerializeField] Transform destination;
    private RaycastHit hitInfo;
    private bool canGrab;

    // Update is called once per frame
    void Update()
    {
        Grab();

        if (Input.GetButtonDown("Fire1"))
        {
            if (canGrab && hitInfo.transform != null)
            {
                Debug.Log("Set down object");
                hitInfo.transform.GetComponent<Rigidbody>().useGravity = true;
                hitInfo.transform.GetComponent<Rigidbody>().isKinematic = false;
                hitInfo.transform.GetComponent<BoxCollider>().isTrigger = false;
            }
            if (Physics.Raycast(firstPersonCamera.transform.position + rayOffset, firstPersonCamera.transform.forward, out hitInfo, range) && hitInfo.transform.tag == "Moveable")
            {
                    Debug.Log("The object name hit is: " + hitInfo.transform.name + " The tag is: " + hitInfo.transform.tag);
                    hitInfo.transform.GetComponent<Rigidbody>().useGravity = false;
                    hitInfo.transform.GetComponent<Rigidbody>().isKinematic = true;
                    hitInfo.transform.GetComponent<BoxCollider>().isTrigger = true;
                    canGrab = !canGrab;                                
            }
        }
    }

    void Grab()
    {
        if (canGrab && hitInfo.transform != null)
        {
            hitInfo.transform.position = destination.transform.position;
            hitInfo.transform.rotation = destination.transform.rotation;
        } 
    }
}
