using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindZone : MonoBehaviour
{
    [SerializeField]
    private float windPower, waitTime;
    [SerializeField]
    private Vector3 direction;
    private List<PlayerController> players;

    private void Awake()
    {
        players = new List<PlayerController>();
        StartCoroutine(ChangeDirection());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            players.Add(other.GetComponent<PlayerController>());
            other.GetComponent<PlayerController>().WindDirection = direction * windPower;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().WindDirection = Vector3.zero;
            players.Remove(other.GetComponent<PlayerController>());
        }
    }

    private IEnumerator ChangeDirection()
    {
        while (true)
        {
            windPower = Random.Range(1f, 3f);
            direction = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
            for (int i = 0; i < players.Count; i++)
                if (players[i] != null)
                    players[i].WindDirection = direction * windPower;
            yield return new WaitForSeconds(waitTime);
        }
    }
}
