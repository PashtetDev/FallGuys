using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPlatform : MonoBehaviour
{
    [SerializeField, Tooltip("The element will move between targetVector and startVector (current position)")]
    private Transform targetVector;
    private Vector3 startVector;
    [SerializeField]
    private float speed;
    private Rigidbody rb;
    private Vector3 direction;

    private void Awake()
    {
        startVector = transform.position;
        direction = (targetVector.position - startVector).normalized * speed;
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, targetVector.position) < 0.05f)
            direction = -(targetVector.position - startVector).normalized * speed;
        if (Vector3.Distance(transform.position, startVector) < 0.05f)
            direction = (targetVector.position - startVector).normalized * speed;
        rb.velocity = direction;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().PlatformDirection = rb.velocity;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().PlatformDirection = Vector3.zero;
        }
    }
}
