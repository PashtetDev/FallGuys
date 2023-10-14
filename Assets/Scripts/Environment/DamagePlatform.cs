using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlatform : ActivePlatform
{
    [SerializeField]
    private MeshRenderer cube;
    private List<PlayerController> players;
    private Animator animator;

    private Material startMaterial;
    private Material orange;
    private Material red;

    public override void Initialization()
    {
        startMaterial = cube.material;
        orange = new Material(cube.material);
        orange.color = activateColor;
        red = new Material(cube.material);
        red.color = newColor;
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
            yield return new WaitForSeconds(waitTime - 4);
            cube.material = startMaterial;
            yield return new WaitForSeconds(waitTime - 1);
        }
    }


    public override void CollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            if (!players.Contains(collision.gameObject.GetComponent<PlayerController>()))
                players.Add(collision.gameObject.GetComponent<PlayerController>());
            StartMyCoroutine(Activate());
        }
    }

    public override void CollisionExit(Collision collision)
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
