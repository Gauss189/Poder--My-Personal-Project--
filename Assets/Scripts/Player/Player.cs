using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private bool powerUp;
    [SerializeField] private bool playerCooldown;
    [SerializeField] private float cooldownTime;

    //private float knockBackForce = 500f;
    private float basicCooldownTime = 1.5f;
    private float powerUpCooldownReduce = 1f;
    private float powerUpDuration = 5f;
    private int playerHealth = 5;
    [SerializeField] private int playerCurrentHealth;

    private Rigidbody playerRb;
    private bool isAttacking;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
        cooldownTime = basicCooldownTime;
        playerCurrentHealth = playerHealth;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;

        if (collision.gameObject.CompareTag("Enemy") && !playerCooldown)
        {
            isAttacking = true;
            Destroy(collision.gameObject);
            playerCooldown = true;
            StartCoroutine(PlayerArmedCooldown());
        }
        else if (collision.gameObject.CompareTag("Enemy") && playerCooldown)
        {
            if (playerCurrentHealth > 0)
            {
                playerCurrentHealth--;

                //Doesn`t work well for now
                // Vector3 knockBack = (collider.transform.position - transform.position).normalized;
                // playerRb.AddForce(knockBack * knockBackForce * Time.fixedDeltaTime, ForceMode.Impulse);
            }
            else if (playerCurrentHealth <= 0)
            {
                //Destroy(gameObject);
                Debug.Log("Player Killed");
            }
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
        isAttacking = false;
    }

    IEnumerator AttackSpeedPowerUpTime()
    {
        yield return new WaitForSeconds(powerUpDuration);
        powerUp = false;
        cooldownTime = basicCooldownTime;
    }

    public bool IsAttacking()
    {
        return isAttacking;
    }
}
