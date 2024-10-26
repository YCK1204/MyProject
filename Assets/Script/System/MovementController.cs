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
        bool isMoving = rb.linearVelocity.x != 0;
        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.linearVelocity.y);
    }

    private void Flip()
    {
        facingDir *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    private void FlipController()
    {
        if (rb.linearVelocity.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (rb.linearVelocity.x < 0 && facingRight)
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
        rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocity.y);
    }

    public void SetInput(float input)
    {
        xInput = input;
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    private void CheckInput() // �Է��� ó���ϴ� �޼��� �߰�
    {
        // �� �޼���� ���⼭ ������� ������, OnPlayerController���� ó��
        // xInput�� ���� �����ϴ� �޼��带 OnPlayerController���� ȣ��
    }
}