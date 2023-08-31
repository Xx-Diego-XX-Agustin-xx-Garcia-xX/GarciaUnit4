using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rigidBody;
    private GameObject focalPoint;
    public float speed = 5.0f;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");
    }
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        rigidBody.AddForce(focalPoint.transform.forward * speed * forwardInput);
    }
}
