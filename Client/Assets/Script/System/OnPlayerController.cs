using UnityEngine;

public class OnPlayerController : MonoBehaviour
{
    private PlayerController playerController;

    void Start()
    {
        // PlayerController 컴포넌트를 가져옵니다.
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        // PlayerController의 Update 메서드를 호출하여 입력을 처리합니다.
        playerController.HandleInput(); // HandleInput 메서드로 변경
    }
}
