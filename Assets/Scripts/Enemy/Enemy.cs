using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float enemySpeed;
    private float enemyRotationSpeed = 800f;

    private GameObject player;
    private bool isRunning;
    private Rigidbody enemyRb;

    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    void Update()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;

        transform.Translate(lookDirection * enemySpeed * Time.deltaTime, Space.World);

        if (lookDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(lookDirection, Vector3.up);

            isRunning = true;

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, enemyRotationSpeed * Time.deltaTime);
        }
    }

    public bool IsRunning()
    {
        return isRunning;
    }
}
