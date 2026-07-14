using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    public DynamicJoystick joy;
    public GameObject effect;
    public Transform cam;

    private AudioSource audioSource;
    public AudioClip playerdiesound;

    private bool isGrounded = false;
    public bool thirdPersonMode;
    public bool isRunButtonPressed = false;
    public float speed = 15f;

    public AudioClip playerjumpSound;
    public float jumpForce = 7f;
    public int jumpcnt=0;

    Animator ani;

    public void OnRunButtonDown() { isRunButtonPressed = true; }
    public void OnRunButtonUp() { isRunButtonPressed = false; }

    public void Die()
    {

        GameManager gameManager = FindFirstObjectByType<GameManager>();
        gameManager.PlaySound(playerdiesound);
        gameObject.SetActive(false);
        gameManager.EndGame();

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "deathcube")Die();
    }
    void Start()
    {

        thirdPersonMode = PlayerPrefs.GetInt("CameraMode", 0) == 1;
        playerRigidbody = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        Debug.Log("thirdPersonMode = " + thirdPersonMode + ", cam = " + cam);

    }

    void Update()
    {
        float keyboardH = Input.GetAxisRaw("Horizontal");
        float keyboardV = Input.GetAxisRaw("Vertical");

        float finalH = joy.Horizontal + keyboardH;
        float finalV = joy.Vertical + keyboardV;

        Vector3 move;

        if (thirdPersonMode && cam != null)
        {
            Vector3 camForward = cam.forward;
            Vector3 camRight = cam.right;

            camForward.y = 0;
            camRight.y = 0;

            camForward.Normalize();
            camRight.Normalize();

            move = camForward * finalV + camRight * finalH;
            if (move.magnitude > 1f) move.Normalize();
        }
        else
        {
            move = new Vector3(finalH, 0, finalV);
        }

        float runSpeed = speed;
        bool isShifting = Input.GetKey(KeyCode.LeftShift) || isRunButtonPressed;

        if (isShifting)
        {
            runSpeed = speed * 3.0f;

        }

        if (move != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);

            Vector3 input = new Vector3(finalH, 0, finalV);
            float inputMagnitude = Mathf.Clamp01(input.magnitude);

            float baseTurnSpeed = 180f;
            float turnSpeed = baseTurnSpeed * inputMagnitude;

            transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRotation,
            turnSpeed * Time.deltaTime
            );

            transform.position += move.normalized * Time.deltaTime * runSpeed;

            ani.SetBool("Walk", true);
            ani.SetBool("Run", isShifting);
        }
        else
        {
            ani.SetBool("Walk", false);
            ani.SetBool("Run", false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
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

    private void OnCollisionExit(Collision collision)
    {

        isGrounded = false;
    }

}

