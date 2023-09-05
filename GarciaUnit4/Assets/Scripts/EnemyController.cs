using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    private Rigidbody enemyRigidBody;
    private GameObject player;
    void Start()
    {
        enemyRigidBody = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }
    void Update()
    {
        Vector3 lookDirection = (player.transform.position -transform.position).normalized;
        enemyRigidBody.AddForce(lookDirection * speed);
    }
}
