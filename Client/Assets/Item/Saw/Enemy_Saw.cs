using UnityEngine;

public class Enemy_Saw : MonoBehaviour
{
    [SerializeField] private float damage = 10f; // 데미지
    [SerializeField] private float speed = 2f;    // 이동 속도
    [SerializeField] private float movementDistance = 5f; // 이동 거리

    private float upEdge;  // 위쪽 경계
    private float downEdge; // 아래쪽 경계
    private bool movingUp; // 위로 이동 중인지 여부

    private void Awake()
    {
        // 초기 경계 설정
        upEdge = transform.position.x - movementDistance;
        downEdge = transform.position.x + movementDistance;
    }

    private void Update()
    {
        // 이동 로직
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
            // 플레이어에게 데미지를 입히는 로직
            PlayerController playerController = collision.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.TakeDamage(damage); // 데미지를 적용
                Debug.Log($"Player hit! Damage: {damage}");
            }
        }
    }
}


