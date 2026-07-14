using UnityEngine;

public class Note : MonoBehaviour
{
    public AudioClip hitSound;
    public GameObject hitEffect;

    void Update()
    {

        transform.position += Time.deltaTime * transform.forward * 2;

        if (transform.position.z < -1f)
        {
            ComboManager.Instance.OnMiss();
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Saber") && other.gameObject.layer == gameObject.layer)
        {

            ComboManager.Instance.AddCombo();

            if (hitEffect != null)
            {

                GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);

                Destroy(effect, 2f);
            }

            if (hitSound != null)
            {
                AudioSource.PlayClipAtPoint(hitSound, transform.position);
            }
            Destroy(gameObject);
        }
    }
}

