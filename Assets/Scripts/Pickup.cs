using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    // Initialize variables
    [SerializeField] Camera firstPersonCamera = null;
    [SerializeField] float range = 2f;
    [SerializeField] Vector3 rayOffset = new Vector3(0,0,0.3f);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(firstPersonCamera.transform.position + rayOffset, firstPersonCamera.transform.forward, out hitInfo, range))
            {
                Debug.Log("Object name: " + hitInfo.transform.name + "Object tag: " + hitInfo.transform.tag); 
            }
        }
    }
}
