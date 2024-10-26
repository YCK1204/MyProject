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

    public GameObject portalPrefab;

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
        bool isMoving = rb.linearVelocity.x != 0; // linearVelocity로 수정
        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.linearVelocity.y); // linearVelocity로 수정
    }

    private void Flip()
    {
        facingDir *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    private void FlipController()
    {
        if (rb.linearVelocity.x > 0 && !facingRight) // linearVelocity로 수정
        {
            Flip();
        }
        else if (rb.linearVelocity.x < 0 && facingRight) // linearVelocity로 수정
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
        rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocity.y); // linearVelocity로 수정
    }

    public void SetInput(float input)
    {
        xInput = input;
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); // linearVelocity로 수정
        }
    }

    private void CheckInput()
    {
        // 예시: 입력 체크 로직 (UI 버튼 클릭 또는 키 입력 등)
        // 예를 들어, xInput에 값 설정
        xInput = Input.GetAxis("Horizontal"); // 수평 입력
    }
    public void CreatePortal()
    {
        if (portalPrefab != null)
        {
            // 포탈 위치 설정
            Vector3 portalPosition = transform.position + transform.up * 2; // 캐릭터 앞에 생성
            GameObject portal = Instantiate(portalPrefab, portalPosition, Quaternion.identity);
            portal.SetActive(true); // 포탈 활성화
        }
        else
        {
            Debug.LogError("Portal prefab is not assigned.");
        }
    }

}
