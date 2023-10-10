using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSController : MonoBehaviour
{
    private ParticleSystem ps;

    public void Initialization()
    {
        ps = GetComponent<ParticleSystem>();
        ps.Play();
        StartCoroutine(WaitAndDestroy());
    }

    private IEnumerator WaitAndDestroy()
    {
        while (ps.isPlaying)
            yield return null;
        Destroy(gameObject);
    }
}
