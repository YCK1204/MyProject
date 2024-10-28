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
        // Collider�� SpriteRenderer ������Ʈ�� �����ɴϴ�.
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // ������Ʈ�� ���� ��� ��� �޽����� ����մϴ�.
        if (boxCollider == null)
        {
            Debug.LogWarning("BoxCollider2D�� �Ҵ���� �ʾҽ��ϴ�.");
        }
        else
        {
            initialColliderSize = boxCollider.size;  // �ʱ� �ݶ��̴� ũ�� ����
        }

        if (spriteRenderer == null)
        {
            Debug.LogWarning("SpriteRenderer�� �Ҵ���� �ʾҽ��ϴ�.");
        }
    }

    private void Update()
    {
        // �ݶ��̴� ũ�⸦ ������Ʈ�մϴ�.
        UpdateColliderSize();
    }

    private void UpdateColliderSize()
    {
        // boxCollider�� spriteRenderer�� null�� �ƴ� ��쿡�� ����
        if (boxCollider != null && spriteRenderer != null)
        {
            // SpriteRenderer�� bounds�� ���� �����Ͽ� �°� ��ȯ�Ͽ� �ݶ��̴� ũ�� ������Ʈ
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
