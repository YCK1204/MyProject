using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    private MainMenuManager mainMenuManager;
    private LobbyManager lobbyManager;
    private IntroManager introManager;

    private void Awake()
    {
        // 싱글톤 패턴 적용
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("UIManager: 이미 다른 UIManager 인스턴스가 존재합니다.");
            Destroy(gameObject);
            return;
        }

        // 각 UI 관리자를 컴포넌트에서 찾기
        mainMenuManager = Object.FindFirstObjectByType<MainMenuManager>();
        lobbyManager = Object.FindFirstObjectByType<LobbyManager>();
        introManager = Object.FindFirstObjectByType<IntroManager>();

        if (lobbyManager == null)
        {
            Debug.LogError("UIManager: LobbyManager가 씬에 없습니다!");
        }
    }

    // 게임 시작 시 UI 초기화
    public void InitializeUI()
    {
        ShowIntroScreen();
    }

    // 인트로 화면 표시 및 영상 재생
    private void ShowIntroScreen()
    {
        introManager.Show();
        introManager.PlayIntroVideo();
        Invoke(nameof(ShowMainMenu), 5f); // 영상 길이에 맞게 5초 후 메인 메뉴로 전환
    }

    // 화면 전환: 메인 메뉴로 이동
    public void ShowMainMenu()
    {
        introManager.Hide();

        if (mainMenuManager != null)
        {
            mainMenuManager.Show();
        }
        else
        {
            Debug.LogError("UIManager: mainMenuManager가 null이어서 메인 메뉴를 표시할 수 없습니다.");
        }

        
    }

    // 화면 전환: 로비 화면으로 이동
    public void ShowLobby()
    {
        if (lobbyManager == null)
        {
            Debug.LogError("UIManager: LobbyManager가 null입니다! 로비 화면을 표시할 수 없습니다.");
            return;
        }

        if (mainMenuManager != null)
        {
            mainMenuManager.Hide();
        }
        else
        {
            Debug.LogError("UIManager: mainMenuManager가 null이어서 메인 메뉴를 숨길 수 없습니다.");
        }

        introManager.Hide();
       
    }
}
