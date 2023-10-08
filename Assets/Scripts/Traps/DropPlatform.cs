using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPlatform : MonoBehaviour
{
    [SerializeField]
    private float waitTime;
    private Coroutine fall;
    private Rigidbody rb;

    [SerializeField]
    private Color activateColor, fallColor;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player") && fall == null)
            fall = StartCoroutine(WaitAndFall());
    }

    private IEnumerator WaitAndFall()
    {
        MeshRenderer myMesh = GetComponent<MeshRenderer>();
        Material activate = new Material(myMesh.material);
        Material fall = new Material(myMesh.material);
        activate.color = activateColor;
        fall.color = fallColor;

        myMesh.material = activate;
        yield return new WaitForSeconds(waitTime);
        myMesh.material = fall;
        gameObject.AddComponent<Rigidbody>();
    }
}
