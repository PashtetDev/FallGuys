using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    [SerializeField]
    private float jumpForce, rotationSpeed;
    [SerializeField]
    private GameObject mesh;
    [SerializeField, Space]
    private CameraHolder cameraHolder;

    [SerializeField, Space]
    private GroundChecker groundChecker;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (groundChecker.onGround)
        {
            Movement();
            if (Input.GetKey(KeyCode.Space))
                Jump();
        }
        MeshRotate(rb.velocity);
    }

    public void MeshRotate(Vector3 targetVector)
    {
        if (targetVector.z != 0)
        {
            float angle = Mathf.Atan(targetVector.x / targetVector.z) * Mathf.Rad2Deg;

            if (targetVector.z <= 0)
                angle += 180;

            mesh.transform.eulerAngles = new Vector3(0, Mathf.LerpAngle((mesh.transform.eulerAngles.y + 360) % 360, (angle + 360) % 360, rotationSpeed * Time.deltaTime), 0);
        }
    }

    private void Movement()
    {
        Vector3 direction = RotateVector(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")), -cameraHolder.transform.eulerAngles.y).normalized;
        rb.velocity = new Vector3(speed * direction.x, rb.velocity.y, speed * direction.y);
    }

    public void Jump()
    {
        if (rb.velocity.y < jumpForce)
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    public static Vector2 RotateVector(Vector2 vector, float angle)
    {
        angle *= Mathf.Deg2Rad;
        return new Vector2(vector.x * Mathf.Cos(angle) - vector.y * Mathf.Sin(angle),
            vector.x * Mathf.Sin(angle) + vector.y * Mathf.Cos(angle));
    }
}
