using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField]
    private float duration, magnitude;
    public static CameraShake instance;
    private Vector3 startPosition;

    private void Awake()
    {
        instance = this;
    }

    public void ShakeCaller()
    {
        StartCoroutine(Shake(duration));
    }

    private IEnumerator Shake(float duration)
    {
        Debug.Log("Shake");
        startPosition = transform.localPosition;
        while (duration > 0)
        {
            duration -= Time.deltaTime;
            yield return null;
            transform.localPosition = startPosition + new Vector3(Random.Range(-1f, 1f) * magnitude, Random.Range(-1f, 1f) * magnitude, Random.Range(-1f, 1f) * magnitude);
        }
        transform.localPosition = startPosition;
    }
}
