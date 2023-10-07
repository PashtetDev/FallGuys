using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPlatform : MonoBehaviour
{
    [SerializeField, Tooltip("The element will move between targetVector and startVector (current position)")]
    private Vector3 targetVector;
    private Vector3 startVector;
    [SerializeField]
    private float speed;
    private Rigidbody rb;
    private Vector3 direction;

    private void Awake()
    {
        startVector = transform.position;
        direction = (targetVector - startVector).normalized * speed;
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, targetVector) < 0.1f)
            direction = -(targetVector - startVector).normalized * speed;
        if (Vector3.Distance(transform.position, startVector) < 0.1f)
            direction = (targetVector - startVector).normalized * speed;
        rb.velocity = direction;
    }
}
