using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PAMControl : MonoBehaviour //Players Animations in Menu Controller
{
    [SerializeField]
    private GameObject ps;
    [SerializeField]
    private Animator animator;

    private void Awake()
    {
        StartCoroutine(Anim());
    }

    private IEnumerator Anim()
    {
        int myCondition = Random.Range(1, 3);
        while (true)
        {
            animator.SetInteger("Condition", myCondition);
            yield return new WaitForSeconds(2.5f);
            myCondition = myCondition % 2 + 1;
            animator.SetInteger("Condition", 0);
            yield return new WaitForSeconds(Random.Range(2f, 4f));
        }
    }

    public void Play()
    {
        StopAllCoroutines();
        animator.SetBool("Play", true);
    }

    private void OnMouseDown()
    {
        if (animator.GetInteger("Condition") < 3)
        {
            StopAllCoroutines();
            if (Random.Range(0, 2) == 0)
                StartCoroutine(Touch1());
            else
                StartCoroutine(Touch2());
        }
    }

    private IEnumerator Touch1()
    {
        animator.SetInteger("Condition", 3);
        Instantiate(ps, transform.position, Quaternion.identity).GetComponent<PSController>().Initialization();
        yield return new WaitForSeconds(1);
        animator.SetInteger("Condition", 0);
        StartCoroutine(Anim());
    }

    private IEnumerator Touch2()
    {
        animator.SetInteger("Condition", 4);
        Instantiate(ps, transform.position, Quaternion.identity).GetComponent<PSController>().Initialization();
        yield return new WaitForSeconds(0.3f);
        animator.SetInteger("Condition", 0);
        StartCoroutine(Anim());
    }
}
