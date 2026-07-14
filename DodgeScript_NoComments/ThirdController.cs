using UnityEngine;

public class ThirdController : MonoBehaviour
{

    private Rigidbody playerRigidbody;
    private Animator ani;
    private AudioSource audioSource;

    public float speed = 8f;
    public float jumpForce = 7f;

    public DynamicJoystick joy;
    public bool isRunButtonPressed = false;

    public AudioClip playerdiesound;
    public AudioClip playerjumpSound;

    public Transform cam;

    private int jumpcnt = 0;
    private bool isGrounded = false;
    private bool isDead = false;

    public void OnRunButtonDown()
    {
        isRunButtonPressed = true;
    }

    public void OnRunButtonUp()
    {
        isRunButtonPressed = false;
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;

        GameManager gameManager = FindFirstObjectByType<GameManager>();

        if (gameManager != null)
        {
            gameManager.PlaySound(playerdiesound);
        }

        gameObject.SetActive(false);

        if (gameManager != null)
        {
            gameManager.EndGame();
        }
    }

    void Start()
    {

        playerRigidbody = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        if (joy == null)
        {
            joy = FindFirstObjectByType<DynamicJoystick>();
        }
    }

    void Update()
    {

        MovePlayer();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    void MovePlayer()
    {

        if (joy == null) return;

        float x = Input.GetAxis("Horizontal") + joy.Horizontal;
        float z = Input.GetAxis("Vertical") + joy.Vertical;

        Vector2 input = new Vector2(x, z);

        if (input.magnitude < 0.15f)
        {
            x = 0f;
            z = 0f;
        }

        Vector3 move;

        if (cam != null)
        {
            Vector3 camForward = cam.forward;
            Vector3 camRight = cam.right;

            camForward.y = 0;
            camRight.y = 0;

            camForward.Normalize();
            camRight.Normalize();

            move = -(camForward * z + camRight * x);

            if (move.magnitude > 1f)
            {
                move.Normalize();
            }
        }
        else
        {

            move = new Vector3(-x, 0, -z);
        }

        float runSpeed = speed;

        bool isShifting =
            Input.GetKey(KeyCode.LeftShift) ||
            isRunButtonPressed ||
            new Vector2(joy.Horizontal, joy.Vertical).magnitude > 0.9f;

        if (isShifting)
        {
            runSpeed = speed * 3.0f;
        }

        if (move.magnitude > 0.01f)
        {

            transform.rotation = Quaternion.LookRotation(move);

            Vector3 velocity = move.normalized * runSpeed;
            velocity.y = playerRigidbody.linearVelocity.y;

            playerRigidbody.linearVelocity = velocity;

            ani.SetBool("Walk", true);
            ani.SetBool("Run", isShifting);
        }
        else
        {

            playerRigidbody.linearVelocity = new Vector3(
                0,
                playerRigidbody.linearVelocity.y,
                0
            );

            ani.SetBool("Walk", false);
            ani.SetBool("Run", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("deathcube") || other.GetComponent<Bullet>() != null)
        {
            Die();
        }
    }

    public void Jump()
    {

        if (jumpcnt < 2)
        {
            jumpcnt++;

            playerRigidbody.linearVelocity = new Vector3(
                playerRigidbody.linearVelocity.x,
                0f,
                playerRigidbody.linearVelocity.z
            );

            playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            if (playerjumpSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(playerjumpSound);
            }

            if (ani != null)
            {
                ani.SetTrigger("Jump");
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.contacts[0].normal.y > 0.7f)
        {
            isGrounded = true;
            jumpcnt = 0;
        }
    }
}
