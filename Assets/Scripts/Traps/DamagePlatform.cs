using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlatform : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer cube;
    private List<PlayerController> players;
    private Coroutine coroutine;
    private Animator animator;

    [SerializeField]
    private Color orangeColor, redColor;

    private Material startMaterial;
    private Material orange;
    private Material red;

    private void Awake()
    {
        startMaterial = cube.material;
        orange = new Material(cube.material);
        orange.color = orangeColor;
        red = new Material(cube.material);
        red.color = redColor;
        animator = GetComponent<Animator>();
        animator.SetBool("Activate", false);
        players = new List<PlayerController>();
    }

    private IEnumerator Activate()
    {
        while (players.Count != 0)
        {
            cube.material = orange;
            animator.SetBool("Activate", false);
            yield return new WaitForSeconds(1);
            cube.material = red;
            animator.SetBool("Activate", true);
            for (int i = 0; i < players.Count; i++)
            {
                players[i].GetDamage(5);
            }
            yield return new WaitForSeconds(1);
            cube.material = startMaterial;
            yield return new WaitForSeconds(4);
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
                    cube.material = startMaterial;
                    coroutine = null;
                }
            }
        }
    }
}
