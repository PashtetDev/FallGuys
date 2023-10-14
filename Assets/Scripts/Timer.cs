using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private UIDrawer uIDrawer;
    private float time;

    public void StartTicksCaller()
    {
        StartCoroutine(StartTicks());
    }

    private IEnumerator StartTicks()
    {
        while (!PlayerController.instance.isLose && !PlayerController.instance.win)
        {
            uIDrawer.RenderTicks(time);
            yield return null;
            time += Time.deltaTime;
        }
    }
}
