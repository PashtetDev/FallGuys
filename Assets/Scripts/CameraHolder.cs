using UnityEngine;

public class CameraHolder : MonoBehaviour
{
    [SerializeField]
    private Vector2 rotationRange;
    [SerializeField]
    private float sensitivity, speed;

    private Vector3 rotation;

    public void CameraMove(Vector3 target)
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Rotation();
            Movement(target);
        }
    }

    private void Rotation()
    {
        Vector2 direction = new Vector2(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));
        rotation += (Vector3)direction * Time.deltaTime * 100 * sensitivity;
        rotation.x = Mathf.Clamp(rotation.x, rotationRange.x, rotationRange.y);
        transform.localEulerAngles = rotation;
    }

    private void Movement(Vector3 target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target, Boost(target) * speed * Time.deltaTime);
    }

    private float Boost(Vector3 target)
    {
        return Mathf.Pow(Mathf.Clamp(Vector3.Distance(transform.position, target), 0.5f, 5f), 2);
    }
}
