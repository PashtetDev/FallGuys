using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPlatform : ActivePlatform
{
    public override void CollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
            StartMyCoroutine(WaitAndFall());
    }

    private void FixedUpdate()
    {
        if (transform.position.y < -20f)
            Destroy(gameObject);
    }

    private IEnumerator WaitAndFall()
    {
        MeshRenderer myMesh = GetComponent<MeshRenderer>();
        Material activate = new Material(myMesh.material);
        Material fall = new Material(myMesh.material);
        activate.color = activateColor;
        fall.color = newColor;

        myMesh.material = activate;
        yield return new WaitForSeconds(waitTime);
        myMesh.material = fall;
        gameObject.AddComponent<Rigidbody>();
    }

    public override void CollisionExit(Collision collision) { }

    public override void Initialization() { }
}
