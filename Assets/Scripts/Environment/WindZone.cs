using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindZone : MonoBehaviour
{
    [SerializeField]
    private float waitTime;
    [SerializeField]
    private Vector3 direction;
    private List<PlayerController> players;
    [SerializeField]
    private GameObject arrow;
    private float windPower;

    public void Awake()
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
            windPower = Random.Range(2f, 4f);
            direction = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
            for (int i = 0; i < players.Count; i++)
                if (players[i] != null)
                    players[i].WindDirection = direction * windPower;
            if (arrow != null)
            {
                if (direction.z > 0)
                    arrow.transform.localEulerAngles = new Vector3(90, Mathf.Atan(direction.x / direction.z) * Mathf.Rad2Deg - 90, 0);
                else
                    arrow.transform.localEulerAngles = new Vector3(90, Mathf.Atan(direction.x / direction.z) * Mathf.Rad2Deg + 90, 0);
            }
            yield return new WaitForSeconds(waitTime);
        }
    }
}
