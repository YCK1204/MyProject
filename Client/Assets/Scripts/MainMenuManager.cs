using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuManager : MonoBehaviour
{
    private UIDocument uiDocument;
    private VisualElement root;
    private Button singlePlayButton;
    private Button multiPlayButton;
    private Button settingsButton;
    private Button exitButton;

    private void Awake()
    {
        uiDocument = GetComponent<UIDocument>();
        if (uiDocument == null)
        {
            Debug.LogError("MainMenuManager: UIDocument가 연결되지 않았습니다!");
            return;
        }

        root = uiDocument.rootVisualElement;
        if (root == null)
        {
            Debug.LogError("MainMenuManager: rootVisualElement를 찾을 수 없습니다!");
            return;
        }

        singlePlayButton = root.Q<Button>("SinglePlayButton");
        multiPlayButton = root.Q<Button>("MultiPlayButton");
        settingsButton = root.Q<Button>("SettingsButton");
        exitButton = root.Q<Button>("ExitButton");

        if (singlePlayButton == null) Debug.LogError("MainMenuManager: SinglePlayButton을 찾을 수 없습니다!");
        if (multiPlayButton == null) Debug.LogError("MainMenuManager: MultiPlayButton을 찾을 수 없습니다!");
        if (settingsButton == null) Debug.LogError("MainMenuManager: SettingsButton을 찾을 수 없습니다!");
        if (exitButton == null) Debug.LogError("MainMenuManager: ExitButton을 찾을 수 없습니다!");

        if (singlePlayButton != null) singlePlayButton.clicked += StartSinglePlayer;
        if (multiPlayButton != null)
        {
            multiPlayButton.clicked += () =>
            {
                if (UIManager.Instance == null)
                {
                    Debug.LogError("MainMenuManager: UIManager.Instance가 null입니다! UIManager가 씬에 존재하는지 확인해주세요.");
                    return;
                }
                UIManager.Instance.ShowLobby();
            };
        }
        if (settingsButton != null) settingsButton.clicked += ShowSettings;
        if (exitButton != null) exitButton.clicked += ExitGame;
    }

    private void StartSinglePlayer()
    {
        Debug.Log("싱글 플레이 시작!");
        UnityEngine.SceneManagement.SceneManager.LoadScene("loading-menu");
    }

    private void ShowSettings()
    {
        Debug.Log("설정 화면 표시!");
    }

    private void ExitGame()
    {
        Debug.Log("게임 종료!");
        Application.Quit();
    }

    public void Show()
    {
        root.style.display = DisplayStyle.Flex;
    }

    public void Hide()
    {
        root.style.display = DisplayStyle.None;
    }
}
