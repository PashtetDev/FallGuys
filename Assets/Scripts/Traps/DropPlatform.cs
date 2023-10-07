using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPlatform : MonoBehaviour
{
    [SerializeField]
    private float waitTime;
    private Coroutine fall;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player") && fall == null)
            fall = StartCoroutine(WaitAndFall());
    }

    private IEnumerator WaitAndFall()
    {
        yield return new WaitForSeconds(waitTime);
        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.None;
    }
}
