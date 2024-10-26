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
            // 캐릭터와 목적지 간의 거리 확인
            if (Vector2.Distance(collision.transform.position, destination.transform.position) > 0.3f)
            {
                // destination에서 0.3만큼 앞의 위치 계산
                Vector2 direction = (destination.transform.position - collision.transform.position).normalized;
                Vector2 offset = direction * 0.5f; // 0.3만큼의 오프셋 생성

                // 새로운 위치 계산 후 캐릭터 이동
                collision.transform.position = (Vector2)destination.transform.position + offset;
            }
        }
    }
}


