using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotor : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Rigidbody rb;
    private float angleY;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        angleY = (angleY + speed * 100 * Time.deltaTime) % 360;
        rb.MoveRotation(Quaternion.Euler(Vector3.up * angleY));
    }
}
