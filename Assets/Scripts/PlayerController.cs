using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] CharacterController characterController = null;
    [SerializeField] float movementSpeed = 5f;

    // Update is called once per frame
    void Update()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        Vector3 move = transform.forward * yAxis + transform.right * xAxis;
        characterController.Move(movementSpeed * Time.deltaTime * move);
    }
}
