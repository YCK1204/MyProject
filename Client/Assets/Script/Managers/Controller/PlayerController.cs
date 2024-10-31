using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody2D rb;
    public Animator anim;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;   // 이동 속도
    [SerializeField] private float jumpForce = 10f;   // 점프 힘

    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 100f;  // 최대 체력
    private float currentHealth;                        // 현재 체력

    [Header("Ground Check")]
    [SerializeField] private float groundCheckDistance = 0.1f; // 바닥 체크 거리
    [SerializeField] private LayerMask groundLayer;            // 바닥 레이어
    private bool isGrounded;                                 // 바닥에 닿아있는지 여부

    [Header("Portal Settings")]
    public GameObject portalPrefab;                          // 포탈 프리팹

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();                   // Rigidbody 초기화
        anim = GetComponentInChildren<Animator>();          // Animator 초기화
        currentHealth = maxHealth;                          // 초기 체력 설정
    }

    private void Update()
    {
        HandleInput();                                      // 입력 처리
        CheckGroundStatus();                                // 바닥 체크
        UpdateAnimator();                                   // 애니메이터 업데이트
    }

    public void HandleInput()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // 수평 입력
        Move(horizontalInput);                               // 이동 처리

        if (Input.GetKeyDown(KeyCode.Space))               // 점프 입력
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.E))                   // 포탈 생성 입력
        {
            CreatePortal();
        }
    }

    private void Move(float input)
    {
        rb.linearVelocity = new Vector2(input * moveSpeed, rb.linearVelocity.y); // 이동 속도 설정
        Flip(input);  // 방향 반전 처리
    }

    private void Jump()
    {
        if (isGrounded)                                     // 바닥에 닿아있을 때만 점프
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); // 점프 속도 설정
        }
    }

    private void CreatePortal()
    {
        if (portalPrefab != null)
        {
            Vector3 portalPosition = transform.position + Vector3.up * 2; // 캐릭터 위에 포탈 생성
            Instantiate(portalPrefab, portalPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Portal prefab is not assigned.");
        }
    }

    private void CheckGroundStatus()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer); // 바닥 체크
    }

    private void UpdateAnimator()
    {
        anim.SetBool("isGrounded", isGrounded); // 애니메이션 상태 업데이트
        // 추가 애니메이션 상태 업데이트 가능
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;                    // 데미지 적용
        if (currentHealth <= 0)
        {
            Die();
        }
        Debug.Log($"Current Health: {currentHealth}");
    }

    private void Die()
    {
        Debug.Log("Player has died.");
        // 사망 처리 로직 추가 가능
    }

    private void Flip(float input)
    {
        if (input > 0) 
        {
            transform.localScale = new Vector3(1, 1, 1); 
        }
        else if (input < 0) 
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
