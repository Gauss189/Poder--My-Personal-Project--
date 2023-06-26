using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private float speed = 14f;
    private float rotationSpeed = 700f;

    private bool isRunning;

    void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;

        isRunning = movementDirection != Vector3.zero;

        float playerSize = .7f;
        bool canMove = !Physics.Raycast(transform.position, movementDirection, playerSize);

        if (canMove)
        {
            transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);
        }

        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    public bool IsRunning()
    {
        return isRunning;
    }
}
