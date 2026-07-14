using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody bulletRigidbody;
    public GameObject effect;
    public AudioClip hitSound;

    public float speed = 20f;

    void Start()
    {
        bulletRigidbody = GetComponent<Rigidbody>();

        bulletRigidbody.linearVelocity = transform.forward * speed;

        Destroy(gameObject, 10f);
    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.tag == "Player")
        {

            Instantiate(effect, other.transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(hitSound, transform.position);

            PlayerController playerController = other.GetComponent<PlayerController>();
            ThirdController thirdController = other.GetComponent<ThirdController>();

            if (playerController != null)
            {
                playerController.Die();
            }
            if (thirdController != null)
            {
                thirdController.Die();
            }
        }

        if(other.tag == "Wall")
        {
            Destroy(gameObject);
        }

    }

}

