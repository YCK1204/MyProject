using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float movementDistance;

    public Collider2D cd;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;
    private Vector2 initialColliderSize;

    private void Awake()
    {
        // Collider와 SpriteRenderer 컴포넌트를 가져옵니다.
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 컴포넌트가 없을 경우 경고 메시지를 출력합니다.
        if (boxCollider == null)
        {
            Debug.LogWarning("BoxCollider2D가 할당되지 않았습니다.");
        }
        else
        {
            initialColliderSize = boxCollider.size;  // 초기 콜라이더 크기 저장
        }

        if (spriteRenderer == null)
        {
            Debug.LogWarning("SpriteRenderer가 할당되지 않았습니다.");
        }
    }

    private void Update()
    {
        // 콜라이더 크기를 업데이트합니다.
        UpdateColliderSize();
    }

    private void UpdateColliderSize()
    {
        // boxCollider와 spriteRenderer가 null이 아닌 경우에만 실행
        if (boxCollider != null && spriteRenderer != null)
        {
            // SpriteRenderer의 bounds를 로컬 스케일에 맞게 변환하여 콜라이더 크기 업데이트
            Vector2 newSize = new Vector2(
                spriteRenderer.sprite.bounds.size.x * transform.localScale.x,
                spriteRenderer.sprite.bounds.size.y * transform.localScale.y
            );

            boxCollider.size = newSize;
            boxCollider.offset = spriteRenderer.bounds.center - transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // collision.GetComponent<Player>().TakeDamage(damage);
        }
    }
}
