using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActivePlatform : MonoBehaviour
{
    public float waitTime;
    public Coroutine coroutine;
    public Color activateColor, newColor;

    private void Awake()
    {
        Initialization();
    }

    public abstract void Initialization();

    private void OnCollisionEnter(Collision collision)
    {
        CollisionEnter(collision);
    }

    private void OnCollisionExit(Collision collision)
    {
        CollisionExit(collision);
    }

    public abstract void CollisionEnter(Collision collision);

    public void StartMyCoroutine(IEnumerator myCoroutine)
    {
        if (coroutine == null)
            coroutine = StartCoroutine(myCoroutine);
    }

    public abstract void CollisionExit(Collision collision);
}
