using UnityEngine;

public class OnPlayerController : MonoBehaviour
{
    private PlayerController playerController;

    void Start()
    {
        // PlayerController ������Ʈ�� �����ɴϴ�.
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        // PlayerController�� Update �޼��带 ȣ���Ͽ� �Է��� ó���մϴ�.
        playerController.HandleInput(); // HandleInput �޼���� ����
    }
}
