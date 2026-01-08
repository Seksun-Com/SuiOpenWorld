using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        // Handle horizontal movement input
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 vectorInput = new(horizontalInput * speed, body.linearVelocity.y);
        body.linearVelocity = vectorInput;

        // Handle player flip
        if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1 * Mathf.Abs(body.transform.localScale.x), body.transform.localScale.y, body.transform.localScale.z);
        else if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(Mathf.Abs(body.transform.localScale.x), body.transform.localScale.y, body.transform.localScale.z);

        // Animation Handle
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", IsGrounded());
    }
    
    void FixedUpdate()
    {
        // Handle jump input
        if (wallJumpCooldown > 0.2f)
        {
            body.gravityScale = 7;

            if (Input.GetKey(KeyCode.Space))
                Jump();
        }
        else
            wallJumpCooldown += Time.deltaTime;
    }
    public bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    public bool OnWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    private void Jump()
    {
        if (IsGrounded())
        {
            body.gravityScale = 0;
            body.linearVelocity = Vector2.zero;
            anim.SetTrigger("jump");
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);
        }
        else if (OnWall() && !IsGrounded())
        {
            wallJumpCooldown = 0;
            body.gravityScale = 0;
            body.linearVelocity = Vector2.zero;
            anim.SetTrigger("jump");
            body.linearVelocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 50f, jumpPower);
        }

    }
}