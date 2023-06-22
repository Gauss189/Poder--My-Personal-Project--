using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   [SerializeField] private float cooldownTime;
    private float basicCooldownTime = 1.5f;
    private float powerUpCooldownReduce = 1f;
    private float powerUpDuration = 5f;
    private float speed = 16f;
    private float rotationSpeed = 800f;

    private Rigidbody playerRb;

    private bool isRunning;
    [SerializeField] private bool powerUp = false;
    [SerializeField] private bool playerCooldown = false;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        cooldownTime = basicCooldownTime;
    }

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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !playerCooldown)
        {
            Destroy(collision.gameObject);
            playerCooldown = true;
            StartCoroutine(PlayerArmedCooldown());
        }
        else if (collision.gameObject.CompareTag("Enemy") && playerCooldown)
        {
            //Destroy(gameObject);
            Debug.Log("Player Killed");
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp") && cooldownTime == basicCooldownTime)
        {
            Destroy(other.gameObject);
            powerUp = true;

            cooldownTime -= powerUpCooldownReduce;
            StartCoroutine(AttackSpeedPowerUpTime());
        }
    }

    IEnumerator PlayerArmedCooldown()
    {
        yield return new WaitForSeconds(cooldownTime);
        playerCooldown = false;
    }

    IEnumerator AttackSpeedPowerUpTime()
    {
        yield return new WaitForSeconds(powerUpDuration);
        powerUp = false;
        cooldownTime = basicCooldownTime;
    }

    public bool IsRunning()
    {
        return isRunning;
    }
}
