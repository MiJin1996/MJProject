using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerControll : MonoBehaviour
{
    public Joystick dynamicJoystick;
    //컴포넌트와 상태 
    private Rigidbody rb;
    private bool isGrounded;
    private Animator animator;

    //🚛이동 설정🚛 
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public float rotationSpeed = 10f;

    //⛱️땅⛱️
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    //🏃🏻‍♂️달리기 설정, 달리지 않을 상태(false)👟
    public float runSpeed = 10f;
    public GameObject runEffectPrefab;

    private bool isResting = false;
    

    //------------------------------------------- 함수 발동 🚀🧑🏾‍🚀
    void Start(){
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update(){
        //💻키보드 입력💻, 🕹️조이 입력(if문)🕹️
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (dynamicJoystick != null){
            if (dynamicJoystick.Horizontal != 0){
                h = dynamicJoystick.Horizontal;
            }

            if (dynamicJoystick.Vertical != 0){
                v = dynamicJoystick.Vertical;
            }
        }

        //🪑쉬기 입력, 쉬기 입력을 받았을때에는 움직이지 않도록 설계💺
        if (Input.GetKeyDown(KeyCode.R)){
            isResting = !isResting;
            animator.SetBool("sitNrest", isResting); //📢 "Animator야, sitNRest라는 Bool 변수의 값을 isResting 값으로 바꿔줘."라는 뜻
        }

        if (isResting){
            h = 0;
            v = 0;
        }

        //↕️이동 방향 만들기↔️, 🗺️방향에 따른 회전(if문)🔄️ :: 흐름:: 키입력 > 쉬냐 안쉬냐 > 맞으면 h, v 0으로 > 아니면 move > 회전 및 이동 > 땅체크 > 점프
        Vector3 move = new Vector3(h, 0, v);

        if (move.sqrMagnitude > 0.01f){
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        //👸🏻플레이어 이동🤴🏻, 🚶🏻‍♀️걷기 애니메이션(bool)🚶🏻‍♀️
        transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);

        bool isMoving = Mathf.Abs(h) > 0.1f || Mathf.Abs(v) > 0.1f;
        animator.SetBool("walk", isMoving);

        //⛱️땅 확인⛱️
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        //🦘점프🦘
        if (isGrounded && Input.GetButtonDown("Jump")){
            rb.AddForce(
                Vector3.up * jumpForce,
                ForceMode.Impulse
            );
        }
    }
}