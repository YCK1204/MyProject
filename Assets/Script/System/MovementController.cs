using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;

    [Header("MoveJump")]
    public float xInput;

    private int facingDir = 1;
    private bool facingRight = true;

    [SerializeField] public float moveSpeed;
    [SerializeField] public float jumpForce;

    [Header("Collision check")]
    public bool isGrounded;
    [SerializeField] public float groundCheckDistance;
    [SerializeField] public LayerMask whatIsGround;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        CheckInput(); 
        CollisionChecks();
        FlipController();
        AnimatorController();
    }

    private void AnimatorController()
    {
        bool isMoving = rb.velocity.x != 0;
        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);
    }

    private void Flip()
    {
        facingDir *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    private void FlipController()
    {
        if (rb.velocity.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (rb.velocity.x < 0 && facingRight)
        {
            Flip();
        }
    }

    private void CollisionChecks()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
    }

    public void SetInput(float input)
    {
        xInput = input;
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void CheckInput() // 입력을 처리하는 메서드 추가
    {
        // 이 메서드는 여기서 사용하지 않지만, OnPlayerController에서 처리
        // xInput을 직접 설정하는 메서드를 OnPlayerController에서 호출
    }
}