using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private bool powerUp;
    [SerializeField] private bool playerCooldown;
    [SerializeField] private float cooldownTime;

    [SerializeField] private float knockBackForce = 10000f;
    [SerializeField] private float knockBackDuration = 0.5f;
    private float basicCooldownTime = 1.5f;
    private float powerUpCooldownReduce = 1f;
    private float powerUpDuration = 5f;
    private int playerHealth = 5;
    [SerializeField] private int playerCurrentHealth;

    private Rigidbody playerRb;
    private bool isAttacking;
    private bool isKnockback;

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
        else if (collision.gameObject.CompareTag("Enemy") && playerCooldown && !isKnockback)
        {
            if (playerCurrentHealth > 0)
            {
                --playerCurrentHealth;

                StartCoroutine(KnockBackCoroutine());
            }
            if (playerCurrentHealth <= 0)
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

    private IEnumerator KnockBackCoroutine()
    {
        isKnockback = true;

        Vector3 knockbackDirection = (transform.position - GameObject.FindGameObjectWithTag("Enemy").transform.position).normalized;
        playerRb.AddForce(knockbackDirection * knockBackForce, ForceMode.Impulse);

        yield return new WaitForSeconds(knockBackDuration);
        isKnockback = false;
    }

    public bool IsAttacking()
    {
        return isAttacking;
    }
}
