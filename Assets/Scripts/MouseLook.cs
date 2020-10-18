using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Protects from duplicate scripts
[DisallowMultipleComponent]

public class MouseLook : MonoBehaviour
{
    // Initialize variables
    [SerializeField] float mouseSense = 300f;
    private float yRotation = 0f;
    private GameObject playerCharacter;
    

    // Start is called before the first frame update
    void Start()
    {
        // Cursor inprovements
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // Find and set player object
        playerCharacter = GameObject.Find("PlayerCharacter");
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSense * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSense * Time.deltaTime;
        
        yRotation -= mouseY;
        yRotation = Mathf.Clamp(yRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);
        playerCharacter.transform.Rotate(Vector3.up * mouseX);
    }
}
