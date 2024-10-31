using UnityEngine;

public class Enemy_Saw : MonoBehaviour
{
    [SerializeField] private float damage = 10f; // ������
    [SerializeField] private float speed = 2f;    // �̵� �ӵ�
    [SerializeField] private float movementDistance = 5f; // �̵� �Ÿ�

    private float upEdge;  // ���� ���
    private float downEdge; // �Ʒ��� ���
    private bool movingUp; // ���� �̵� ������ ����

    private void Awake()
    {
        // �ʱ� ��� ����
        upEdge = transform.position.x - movementDistance;
        downEdge = transform.position.x + movementDistance;
    }

    private void Update()
    {
        // �̵� ����
        if (movingUp)
        {
            if (transform.position.x > upEdge)
            {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
            {
                movingUp = false;
            }
        }
        else
        {
            if (transform.position.x < downEdge)
            {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
            {
                movingUp = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // �÷��̾�� �������� ������ ����
            PlayerController playerController = collision.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.TakeDamage(damage); // �������� ����
                Debug.Log($"Player hit! Damage: {damage}");
            }
        }
    }
}


