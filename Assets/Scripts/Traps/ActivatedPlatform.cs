using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatedPlatform : MonoBehaviour
{
    private List<PlayerController> players;
    private Coroutine coroutine;

    private void Awake()
    {
        players = new List<PlayerController>();
    }

    private IEnumerator Activate()
    {
        while (players.Count != 0)
        {
            yield return new WaitForSeconds(1);
            for (int i = 0; i < players.Count; i++)
            {
                players[i].GetDamage(1);
            }
            yield return new WaitForSeconds(5);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            if (!players.Contains(collision.gameObject.GetComponent<PlayerController>()))
                players.Add(collision.gameObject.GetComponent<PlayerController>());
            if (coroutine == null)
                coroutine = StartCoroutine(Activate());
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            if (players.Contains(collision.gameObject.GetComponent<PlayerController>()))
            {
                players.Remove(collision.gameObject.GetComponent<PlayerController>());
                if (players.Count == 0)
                {
                    StopCoroutine(coroutine);
                    coroutine = null;
                }
            }
        }
    }
}
