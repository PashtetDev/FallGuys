using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PAMControl : MonoBehaviour //Players Animations in Menu Controller
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(Anim());
    }

    private IEnumerator Anim()
    {
        int myCondition = 1;
        while (true)
        {
            animator.SetInteger("Condition", myCondition);
            yield return new WaitForSeconds(1);
            myCondition = myCondition % 2 + 1;
            animator.SetInteger("Condition", 0);
            yield return new WaitForSeconds(Random.Range(3f, 5f));
        }
    }

    public void Play()
    {
        StopAllCoroutines();
        animator.SetBool("Play", true);
    }
}
