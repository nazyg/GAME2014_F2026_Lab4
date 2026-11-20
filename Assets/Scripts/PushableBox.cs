using UnityEngine;

// crate that the player can push and also stand on
// (it has a dynamic rigidbody and is on the Ground layer)
public class PushableBox : MonoBehaviour
{
    private Vector3 startPosition;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    // if the box falls off the map it goes back to where it started
    public void ResetBox()
    {
        transform.position = startPosition;
        rb.linearVelocity = Vector2.zero;
    }
}
