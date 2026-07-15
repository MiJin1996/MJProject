using UnityEngine;

public class SnailPatrol : MonoBehaviour
{
    public float speed = 1f;

    private Vector3 startPos;
    private float moveDistance;
    private int direction = 1;
    private Animator animator; // 달팽이 애니메이터도 게임 종료 시 멈추도록 하기 위해 추가함

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()// OnEnable :: 매번 호출
    {
        direction = 1;

        moveDistance = GetComponentInParent<SpriteRenderer>().bounds.size.x / 2f;

        Vector3 pos = transform.localPosition;
        pos.x = Random.Range(-moveDistance, moveDistance);
        transform.localPosition = pos;

        startPos = transform.localPosition;


/*
        startPos = transform.localPosition; // 달팽이 처음 위치 저장
        direction = 1;

        moveDistance = GetComponentInParent<SpriteRenderer>().bounds.size.x / 2f; // bounds:: 플랫폼의 가로 크기를 자동으로 읽기
        */
    }

    private void Update()
    {
        if (GameManager.instance != null && GameManager.instance.isGameover) // 게임 오버 상태이면 달팽이 이동 중지 :: instance는 가르키는 대상:: 게임 안에 하나만 존재하는 gameobj=싱클톤
            {
            animator.enabled = false;
            return;
            }
        transform.localPosition += Vector3.right * direction * speed * Time.deltaTime; // 달팽이 현 위치 = (오른쪽 또는 왼쪽)으로 매 프레임 조금씩 이동시키는 코드

        float distance = transform.localPosition.x - startPos.x;

        if ((direction == 1 && distance >= moveDistance) || (direction == -1 && distance <= -moveDistance)) //distance >= moveDistance || distance <= -moveDistance)
        {
            direction *= -1;
            Flip();
        }
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}