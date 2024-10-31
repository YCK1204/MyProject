using UnityEngine;

public class OnPlayerController : MonoBehaviour
{
    private MovementController movementController;

    void Start()
    {
        // MovementController ������Ʈ�� �����ɴϴ�.
        movementController = GetComponent<MovementController>();
    }

    void Update()
    {
        // MovementController�� �Է� ó���� ����
        movementController.ProcessInput();
    }
}

