using UnityEngine;

public class MovementController : MonoBehaviour, IPlayerController
{
    public Rigidbody2D rb;
    public Animator anim;

    [Header("Move/Jump")]
    public float moveSpeed;
    public float jumpForce;

    [Header("Collision Check")]
    public bool isGrounded;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;

    public GameObject portalPrefab;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // 매 프레임마다 입력 처리 및 충돌 체크
        ProcessInput();
        CollisionChecks();
        AnimatorController();
    }

    public void ProcessInput()
    {
        // 수평 입력
        float xInput = Input.GetAxis("Horizontal");
        Move(xInput); // Move 메서드를 통해 이동 처리

        // 점프 및 포탈 생성 입력 처리
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            CreatePortal();
        }
    }

    private void Move(float input)
    {
        rb.linearVelocity = new Vector2(input * moveSpeed, rb.linearVelocity.y); // Rigidbody의 속도 설정
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); // 수직 속도 설정
        }
    }

    public void CreatePortal()
    {
        if (portalPrefab != null)
        {
            Vector3 portalPosition = transform.position + Vector3.up * 2; // 캐릭터 위에 포탈 생성
            GameObject portal = Instantiate(portalPrefab, portalPosition, Quaternion.identity);
            portal.SetActive(true);
        }
        else
        {
            Debug.LogError("Portal prefab is not assigned.");
        }
    }

    private void CollisionChecks()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround); // 바닥 체크
    }

    private void AnimatorController()
    {
        // 애니메이션 상태 관리 코드 (필요시 추가)
    }
}
