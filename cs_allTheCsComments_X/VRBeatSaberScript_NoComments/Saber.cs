using UnityEngine;

public class Saber : MonoBehaviour
{
    public TrailRenderer trail;
    public LayerMask layer;
    Vector3 prevPos;
    public AudioClip hitSound;

    void Start()
{
        prevPos = transform.position;

        if (trail != null)
        {
            trail.Clear();
            trail.emitting = true;
        }
}

 void Update()
 {
     RaycastHit hit;

     if (Physics.Raycast(transform.position, transform.forward, out hit, 2, layer))
{
 Vector3 v1 = transform.position - prevPos;

 if(Vector3.Angle(v1, hit.transform.up) > 130)
 {
    if (hitSound != null)
    {
        AudioSource.PlayClipAtPoint(hitSound, hit.transform.position);
    }

    ComboManager.Instance.AddCombo();
                Note note = hit.transform.GetComponent<Note>();
                if (note != null && note.hitEffect != null)
                {

                    GameObject effect = Instantiate(note.hitEffect, hit.transform.position, Quaternion.identity);
                    Destroy(effect, 2f);
                }
                Destroy(hit.transform.gameObject);
 }
}
prevPos = transform.position;
 }
}

