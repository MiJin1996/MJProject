using UnityEngine;

// PlayerController는 플레이어 캐릭터로서 Player 게임 오브젝트를 제어한다.
public class PlayerController : MonoBehaviour {

   public AudioClip deathClip; // 사망시 재생할 오디오 클립
   public float jumpForce = 700f; // 점프 힘
   public GameObject PlayerJumpEffectPrefab; // 점프 이펙트 프리팹

   private int jumpCount = 0; // 누적 점프 횟수
   private bool isGrounded = false; // 바닥에 닿았는지 나타냄
   private bool isDead = false; // 사망 상태

   private Rigidbody2D playerRigidbody; // 사용할 리지드바디 컴포넌트
   private Animator animator; // 사용할 애니메이터 컴포넌트
   private AudioSource playerAudio; // 사용할 오디오 소스 컴포넌트

  

   private void Start() {
       // 초기화
       // 필요한 컴포넌트를 가져옴, 예: Rigidbody2D, Animator, AudioSource
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
   }

   private void Update() {
       // 사용자 입력을 감지하고 점프하는 처리
       if (isDead) 
       {
            // 사망 시 처리를 더 이상 진행하지 않고 종료
           return;
       }

       // 마우스 왼쪽 버튼을 눌렀으면 && 최대 점프 회수 (2)에 도달하지 않았다면
       if(Input.GetMouseButtonDown(0) && jumpCount < 5) 
       {
           
           jumpCount++;
           playerRigidbody.linearVelocity = Vector2.zero; // 점프 직전에 속도를 순간적으로 제로(0, 0)로 변경
           playerRigidbody.AddForce(new Vector2(0, jumpForce)); // 위로 점프 힘을 가함
           // 오디오 소스 재생
            playerAudio.Play(); // playerAudio.PlayOneShot(jumpClip) 이 더 좋은 방법.
                                // 현재 방법은 다른 오디오가 플레이 되면 점프 사운드가 끊길 수 있음.
     if (PlayerJumpEffectPrefab != null)
    {
        Destroy(Instantiate(PlayerJumpEffectPrefab, transform.position, Quaternion.identity), 2f);
    }

    playerAudio.Play();
       }
        else if (Input.GetMouseButtonUp(0) && playerRigidbody.linearVelocity.y > 0) 
        {
            // 마우스 왼쪽 버튼에서 손을 뗐을 때 && 플레이어가 상승 중이라면
            // 상승 중인 경우 점프를 짧게 만들어주는 처리
            playerRigidbody.linearVelocity = playerRigidbody.linearVelocity * 0.5f;
        }
        
        // 애니메이터의 Grounded 파라미터를 isGrounded 값으로 갱신
        animator.SetBool("Grounded", isGrounded);
   }

   private void Die() {
       // 사망 처리
       // 애니메이터의 Die 트리거 파라미터를 셋
        animator.SetTrigger("Die");
            
        // 오디오 소스에 할당된 오디오 클립을 deathClip으로 변경하고 재생
        playerAudio.clip = deathClip;
        // 사망 효과음 재생
        playerAudio.Play();

        // 속도를 제로(0,0)로 변경
        playerRigidbody.linearVelocity = Vector2.zero;
       // 사망 상태로 true 로 변경
        isDead = true;
        GameManager.instance.OnPlayerDead();  //xx
    }

   private void OnTriggerEnter2D(Collider2D other) {
       // 트리거 콜라이더를 가진 장애물과의 충돌을 감지
        // 충돌한 상대방의 태그가 Dead이며 아직 사망하지 않았다면 Die() 실행
       
        if (other.tag == "Dead" && !isDead)
        {
            Die();
        }
   }

   private void OnCollisionEnter2D(Collision2D collision) {
       // 바닥에 닿았음을 감지하는 처리
       // 어떤 콜라이더와 닿았으며, 충돌 표면이 위쪽을 보고 있으면
       if (collision.contacts[0].normal.y > 0.7f) 
       {
           // 바닥에 닿았음을 감지하는 처리
           isGrounded = true; // 바닥에 닿았으면 isGrounded를 true로 변경
           jumpCount = 0; // 점프 횟수 초기화
       }
   }

   private void OnCollisionExit2D(Collision2D collision) {
       // 바닥에서 벗어났음을 감지하는 처리
       // 어떤 콜라이더에서 떼어진 경우 isGrounded를 false로 변경
       isGrounded = false;
   }
}
/*
 if (PlayerJumpEffectPrefab != null)
        {
            Debug.Log("이펙트 위치: " + transform.name);
            Instantiate(PlayerJumpEffectPrefab, transform.position, Quaternion.identity); // 점프 이펙트 생성 그 후 게임 오브젝트 비활성화
        }
        */