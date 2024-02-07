using System.Collections;
using UnityEngine;

public class PAMControl : MonoBehaviour //Players Animations in Menu
{
    [SerializeField]
    private Material playerMaterial;
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
                StartCoroutine(Touch(3, 1f));
            else
                StartCoroutine(Touch(4, 0.3f));
        }
    }

    private IEnumerator Touch(int condition, float waitTime)
    {
        animator.SetInteger("Condition", condition);
        ps.GetComponent<ParticleSystem>().startColor = playerMaterial.color;
        Instantiate(ps, transform.position, Quaternion.identity).GetComponent<PSController>().Initialization();
        yield return new WaitForSeconds(waitTime);
        animator.SetInteger("Condition", 0);
        StartCoroutine(Anim());
    }

}
