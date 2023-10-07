using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;
    public float speed;
    [SerializeField]
    private float jumpForce, rotationSpeed;
    [SerializeField]
    private GameObject mesh;
    [SerializeField, Space]
    private GroundChecker groundChecker;
    [SerializeField, Space]
    private CameraHolder cameraHolder;
    [SerializeField]
    private int maxHealth;
    private int health;
    private Vector3 movementDirection;
    public Vector3 windDirection;

    private void Awake()
    {
        animator = mesh.GetComponent<Animator>();
        health = maxHealth;
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
        AnimatorController();
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

    private void AnimatorController()
    {
        if (groundChecker.onGround)
            animator.SetBool("OnGround", true);
        else
            animator.SetBool("OnGround", false);
        if (rb.velocity == Vector3.zero)
            animator.SetBool("Walk", false);
        else
            animator.SetBool("Walk", true);
    }

    private void Movement()
    {
        movementDirection = RotateVector(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")), -cameraHolder.transform.eulerAngles.y).normalized;
        rb.velocity = new Vector3(speed * movementDirection.x, rb.velocity.y, speed * movementDirection.y) + windDirection;
    }

    public void Jump()
    {
        if (rb.velocity.y < jumpForce)
        {
            animator.SetBool("OnGround", false);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    public static Vector2 RotateVector(Vector2 vector, float angle)
    {
        angle *= Mathf.Deg2Rad;
        return new Vector2(vector.x * Mathf.Cos(angle) - vector.y * Mathf.Sin(angle),
            vector.x * Mathf.Sin(angle) + vector.y * Mathf.Cos(angle));
    }

    private void Death()
    {
        health = 0;
        Debug.Log("Death");
    }

    public void GetDamage(int damage)
    {
        if (health != 0)
        {
            if (damage >= health)
                Death();
            else
                health -= damage;
        }
    }

    public Vector3 WindDirection
    {
        get { return windDirection; }
        set { windDirection = value; }
    }
}
