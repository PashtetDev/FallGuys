using System.Collections;
using UnityEngine;

public class UFO : MonoBehaviour
{
    [SerializeField]
    private GameObject glass;
    [SerializeField]
    private GameObject indicators;

    private float angle;

    private void FixedUpdate()
    {
        angle += 90 * Time.deltaTime;
        indicators.transform.localEulerAngles = new Vector3(0, 0, angle % 360);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().SitInUFO(gameObject);
            StartCoroutine(GlassRotate(other.gameObject));
        }
    }

    private IEnumerator GlassRotate(GameObject target)
    {
        Vector3 startVector = glass.transform.localEulerAngles;
        startVector.z = target.transform.localEulerAngles.z;
        glass.transform.localEulerAngles = startVector;
        yield return new WaitForSeconds(1);
        GetComponent<Animator>().SetBool("Open", true);
        StartCoroutine(ToSky());
    }

    private IEnumerator ToSky()
    {
        yield return new WaitForSeconds(2);
        float duration = 5f;
        float speed = 10f;
        while (duration > 0)
        {
            duration -= Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, Vector3.one * 1000, speed * Time.deltaTime);
            speed += 150f * Time.deltaTime;
            yield return null;
        }
    }
}
