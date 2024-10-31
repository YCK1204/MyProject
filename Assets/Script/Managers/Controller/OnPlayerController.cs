using UnityEngine;

public class OnPlayerController : MonoBehaviour
{
    private MovementController movementController;

    void Start()
    {
        // MovementController 컴포넌트를 가져옵니다.
        movementController = GetComponent<MovementController>();
    }

    void Update()
    {
        // MovementController에 입력 처리를 위임
        movementController.ProcessInput();
    }
}

