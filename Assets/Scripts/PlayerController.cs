using UnityEngine;

// moves the fox with the joystick, jumps with the jump button
// and picks the right animation (idle, run, jump, fall)
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float jumpForce = 13f;

    [Header("Controls")]
    [SerializeField] private Joystick joystick;
    [SerializeField] private JumpButton jumpButton;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    // has to match the numbers in the "State" parameter in the animator
    private enum PlayerState { Idle = 0, Run = 1, Jump = 2, Fall = 3 }

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool isGrounded;
    private Vector3 respawnPoint;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // first respawn point is where the player starts
        respawnPoint = transform.position;
    }

    void Update()
    {
        // small circle under the feet to see if we are on the ground (tiles or the box)
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // left / right from the joystick
        float x = joystick.Horizontal;
        rb.linearVelocity = new Vector2(x * moveSpeed, rb.linearVelocity.y);

        // turn the fox to the way it is moving
        if (x > 0.01f) spriteRenderer.flipX = false;
        else if (x < -0.01f) spriteRenderer.flipX = true;

        // jump only if we are on the ground
        if (jumpButton.WasPressed() && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        UpdateAnimation(x);
    }

    private void UpdateAnimation(float x)
    {
        PlayerState state;

        if (!isGrounded)
        {
            // going up = jump, going down = fall
            if (rb.linearVelocity.y > 0.1f) state = PlayerState.Jump;
            else state = PlayerState.Fall;
        }
        else if (Mathf.Abs(x) > 0.1f)
        {
            state = PlayerState.Run;
        }
        else
        {
            state = PlayerState.Idle;
        }

        animator.SetInteger("State", (int)state);
    }

    // checkpoint calls this
    public void SetCheckpoint(Vector3 point)
    {
        respawnPoint = point;
    }

    // dead plane calls this
    public void Respawn()
    {
        transform.position = respawnPoint;
        rb.linearVelocity = Vector2.zero;
    }

    // so i can see the ground check circle in the scene view
    void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
