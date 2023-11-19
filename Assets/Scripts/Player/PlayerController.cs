using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;
    public float speed;
    [SerializeField]
    private int maxHealth;
    [SerializeField]
    private float jumpForce, rotationSpeed;
    [SerializeField]
    private GameObject mesh;
    [SerializeField, Space]
    private GroundChecker groundChecker;
    [SerializeField, Space]
    private CameraHolder cameraHolder;
    private int health;
    private Vector3 movementDirection;
    private Vector3 windDirection, platformDirection;
    [SerializeField]
    private UIDrawer uiDrawer;
    public static PlayerController instance;
    [HideInInspector]
    public bool isLose, win;
    [SerializeField]
    private GameObject damageParticle;
    private GameObject ufo;
    private float toUfoSpeed;
    [SerializeField]
    private Timer timer;

    private void Awake()
    {
        win = false;
        isLose = false;
        instance = this;
        animator = mesh.GetComponent<Animator>();
        health = maxHealth;
        rb = GetComponent<Rigidbody>();
    }

    public void SitInUFO(GameObject ufo)
    {
        win = true;
        this.ufo = ufo;
        toUfoSpeed = Vector3.Distance(transform.position, ufo.transform.position);
        StartCoroutine(ToUFO());
    }

    private IEnumerator ToUFO()
    {
        float duration = 2f;
        while (duration > 0)
        {
            MovementToUFO();
            duration -= Time.deltaTime;
            yield return null;
        }
        uiDrawer.WinRender();
        Cursor.lockState = CursorLockMode.None;
        mesh.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (!isLose && !win)
        {
            if (transform.position.y < -20f && !isLose)
            {
                Death();
            }
            if (groundChecker.onGround)
            {
                Movement();
                if (Input.GetKey(KeyCode.Space))
                    Jump();
            }
            MeshRotate(rb.velocity);
            cameraHolder.CameraMove(transform.position);
        }
        AnimatorController();
    }

    private void MovementToUFO()
    {
        if (ufo != null)
        {
            Destroy(rb);
            if (Vector3.Distance(transform.position, ufo.transform.position) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, ufo.transform.position, toUfoSpeed * Time.deltaTime);
                MeshRotate((ufo.transform.position - mesh.transform.position).normalized);
            }
        }
    }

    public void MeshRotate(Vector3 targetVector)
    {
        if (targetVector.z != 0 || targetVector.x != 0)
        {
            float angle = Mathf.Atan(targetVector.x / targetVector.z) * Mathf.Rad2Deg;

            if (targetVector.z < 0)
                angle += 180;

            mesh.transform.eulerAngles = new Vector3(0, Mathf.LerpAngle((mesh.transform.eulerAngles.y + 360) % 360, (angle + 360) % 360, rotationSpeed * Time.deltaTime), 0);
        }
    }

    private void AnimatorController()
    {
        if (rb != null)
        {
            if (rb.velocity.y < -jumpForce * 1.5f)
                animator.SetBool("Fall", true);
            else
                animator.SetBool("Fall", false);

            Vector3 movement = rb.velocity - platformDirection;
            if ((movement.x == 0 || movement.z == 0))
                animator.SetBool("Walk", false);
            else
                animator.SetBool("Walk", true);
        }
        else
            animator.SetBool("JumpInUfo", true);

        if (groundChecker.onGround)
            animator.SetBool("OnGround", true);
        else
            animator.SetBool("OnGround", false);
    }

    private void Movement()
    {
        movementDirection = RotateVector(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")), -cameraHolder.transform.eulerAngles.y).normalized;
        rb.velocity = new Vector3(speed * movementDirection.x, rb.velocity.y, speed * movementDirection.y) + windDirection + platformDirection;
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
        isLose = true;
        health = 0;
        animator.SetBool("Death", true);
        uiDrawer.UpdateHealth(0);
        uiDrawer.LoseRender();
        Cursor.lockState = CursorLockMode.None;
    }

    public void GetDamage(int damage)
    {
        if (health != 0)
        {
            if (damage >= health)
                Death();
            else
                health -= damage;
            uiDrawer.UpdateHealth((float)health / maxHealth);
            Instantiate(damageParticle, transform.position + Vector3.up * 0.5f, Quaternion.identity).GetComponent<PSController>().Initialization();
        }
    }

    public Vector3 WindDirection
    {
        get { return windDirection; }
        set { windDirection = value; }
    }
    public Vector3 PlatformDirection
    {
        get { return platformDirection; }
        set { platformDirection = value; }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Start") && timer != null)
        {
            timer.StartTicksCaller();
            timer = null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Rotor"))
        {
            rb.AddForce((transform.position - collision.contacts[0].point).normalized * 40, ForceMode.Impulse);
        }
    }
}
