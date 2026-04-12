using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;

    [Header("Collision Checks")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private float groundedStabilityTime = 0.05f;

    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance = 0.5f;
    [SerializeField] private float wallSlideSpeed = 2f;
    [SerializeField] private float respawnBufferTime = 0.2f;
    public bool TouchedWall;
    private Rigidbody2D rb;
    private Animator anim;
    private bool isGrounded = true;
    private bool isFacingRight = true;
    private bool wasTouchingWall;
    private bool wasGrounded;
    private bool Jump;
    private bool DoubleJump;
    private bool lockMovementUntilLand;
    private bool initialFacingRight;
    private bool jumpQueued;
    private float lastGroundedTime;
    private float respawnBufferEndTime;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        wasGrounded = isGrounded;
        lastGroundedTime = Time.time;
        initialFacingRight = transform.localScale.x >= 0f;
        isFacingRight = initialFacingRight;
    }
    private void Move()
    {
        float moveDirection = isFacingRight ? 1f : -1f;
        rb.velocity = new Vector2(moveDirection * speed, rb.velocity.y);
    }
    private void GroundCheck()
    {
        bool rawGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (rawGrounded)
        {
            lastGroundedTime = Time.time;
        }

        isGrounded = rawGrounded || (Time.time - lastGroundedTime <= groundedStabilityTime);
    }
    private void WallCheck()
    {
        bool isTouchingWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckDistance, wallLayer);
        TouchedWall = isTouchingWall;
    }
    private void WallSlide()
    {
        if (TouchedWall && !isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -wallSlideSpeed));
            anim.SetBool("WallSlide", true);
        }
        else
        {
            anim.SetBool("WallSlide", false);   
        }
    }
    private void Turn()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
    private void Jumping()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            Jump = true;
            DoubleJump = false;
            anim.ResetTrigger("Land");
            anim.SetTrigger("Jump");
            anim.SetFloat("MoveY", rb.velocity.y);
        }
        else if (Jump && !DoubleJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            DoubleJump = true;
            anim.ResetTrigger("Land");
            anim.SetTrigger("DoubleJump");
        }
    }

    

    public void HandleRespawn()
    {
        isFacingRight = initialFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x = Mathf.Abs(localScale.x) * (initialFacingRight ? 1f : -1f);
        transform.localScale = localScale;

        TouchedWall = false;
        wasTouchingWall = false;
        lockMovementUntilLand = false;
        jumpQueued = false;
        lastGroundedTime = Time.time;
        respawnBufferEndTime = Time.time + respawnBufferTime;
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jumpQueued = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GroundCheck();
        WallCheck();
        WallSlide();

        if (jumpQueued)
        {
            Jumping();
            jumpQueued = false;
        }

        if (!isGrounded && TouchedWall)
        {
            lockMovementUntilLand = true;
        }

        if (!wasGrounded && isGrounded && rb.velocity.y <= 0.05f)
        {
            anim.SetTrigger("Land");
            Jump = false;
            DoubleJump = false;
            lockMovementUntilLand = false;
        }
        if (Time.time >= respawnBufferEndTime && TouchedWall && !wasTouchingWall)
        {
            Turn();
        }
        wasGrounded = isGrounded;
        wasTouchingWall = TouchedWall;

        if (Time.time >= respawnBufferEndTime && !lockMovementUntilLand)
        {
            Move();
        }
        else
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }

        if (wallCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(wallCheck.position, wallCheckDistance);
        }
    }
}
