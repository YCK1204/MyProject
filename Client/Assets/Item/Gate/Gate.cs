using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private GameObject destination;
    GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // ĳ���Ϳ� ������ ���� �Ÿ� Ȯ��
            if (Vector2.Distance(collision.transform.position, destination.transform.position) > 0.3f)
            {
                // destination���� 0.3��ŭ ���� ��ġ ���
                Vector2 direction = (destination.transform.position - collision.transform.position).normalized;
                Vector2 offset = direction * 0.5f; // 0.3��ŭ�� ������ ����

                // ���ο� ��ġ ��� �� ĳ���� �̵�
                collision.transform.position = (Vector2)destination.transform.position + offset;
            }
        }
    }
}


