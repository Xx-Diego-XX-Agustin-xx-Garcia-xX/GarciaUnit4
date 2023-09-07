using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rigidBody;
    private GameObject focalPoint;
    private GameObject tmpRocket;
    private Coroutine powerupCountdown;
    public GameObject powerupIndicator;
    public GameObject rocketPrefab;
    public PowerUpType currentPowerUp = PowerUpType.None;
    public float speed = 5.0f;
    public float powerupStrength = 25.0f;
    public bool hasPowerup = true;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");
    }
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        rigidBody.AddForce(focalPoint.transform.forward * speed * forwardInput);
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
        if (currentPowerUp == PowerUpType.Rockets && Input.GetKeyDown(KeyCode.F))
        {
            LaunchRockets();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            hasPowerup = true;
            currentPowerUp = other.gameObject.GetComponent<PowerUp>().powerUpType;
            Destroy(other.gameObject);
            powerupIndicator.gameObject.SetActive(true);
            if (powerupCountdown != null)
            {
                StopCoroutine(powerupCountdown);
            }
            powerupCountdown = StartCoroutine(PowerupCountdownRoutine());
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && currentPowerUp == PowerUpType.Pushback)
        {
            Rigidbody enemyRigidBody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 fromPlayer = (collision.gameObject.transform.position - transform.position);
            Debug.Log("Collided with " + collision.gameObject.name + " with powerup set to " + currentPowerUp.ToString());
            enemyRigidBody.AddForce(fromPlayer * powerupStrength, ForceMode.Impulse);
        }
    }
    void LaunchRockets()
    {
        foreach(var enemy in FindObjectsOfType<EnemyController>())
        {
            tmpRocket = Instantiate(rocketPrefab, transform.position + Vector3.up, Quaternion.identity);
            tmpRocket.GetComponent<RocketBehavior>().Fire(enemy.transform);
        }
    }
    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
        currentPowerUp = PowerUpType.None;
    }
}
