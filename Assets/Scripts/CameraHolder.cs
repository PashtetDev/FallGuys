using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHolder : MonoBehaviour
{
    [SerializeField]
    private float sensitivity;
    private Vector3 rotation;

    [SerializeField]
    private PlayerController player;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        Rotation();
        Movement();
    }

    private void Rotation()
    {
        Vector2 direction = new Vector2(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));
        rotation += (Vector3)direction * Time.deltaTime * 100 * sensitivity;
        rotation.x = Mathf.Clamp(rotation.x, -10, 60);
        transform.localEulerAngles = rotation;
    }

    private void Movement()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, player.speed * Time.deltaTime);
    }
}
