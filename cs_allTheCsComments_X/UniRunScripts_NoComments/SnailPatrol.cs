using UnityEngine;

public class SnailPatrol : MonoBehaviour
{
    public float speed = 1f;

    private Vector3 startPos;
    private float moveDistance;
    private int direction = 1;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        direction = 1;

        moveDistance = GetComponentInParent<SpriteRenderer>().bounds.size.x / 2f;

        Vector3 pos = transform.localPosition;
        pos.x = Random.Range(-moveDistance, moveDistance);
        transform.localPosition = pos;

        startPos = transform.localPosition;

    }

    private void Update()
    {
        if (GameManager.instance != null && GameManager.instance.isGameover)
            {
            animator.enabled = false;
            return;
            }
        transform.localPosition += Vector3.right * direction * speed * Time.deltaTime;

        float distance = transform.localPosition.x - startPos.x;

        if ((direction == 1 && distance >= moveDistance) || (direction == -1 && distance <= -moveDistance))
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
