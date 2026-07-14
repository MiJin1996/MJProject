using UnityEngine;

public class BackgroundLoop : MonoBehaviour {
    private float width;

    private void Awake() {

        BoxCollider2D bgCollider=GetComponent<BoxCollider2D>();
        width=bgCollider.size.x;
    }

    private void Update() {

        if (transform.position.x <= -width)
        { Reposition(); }
    }

    private void Reposition() {

        Vector2 offset = new Vector2(width * 3f, 0);
        transform.position=(Vector2) transform.position + offset;

    }
}
