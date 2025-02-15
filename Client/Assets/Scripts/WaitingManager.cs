using UnityEngine;
using UnityEngine.UIElements;

public class WaitingManager : MonoBehaviour
{
    private VisualTreeAsset waitingScreen;
    private UIDocument uiDocument;

    void Awake()
    {
        uiDocument = GetComponent<UIDocument>();
        waitingScreen = Resources.Load<VisualTreeAsset>("WaitingScreen");
    }

    public void Show()
    {
        var root = uiDocument.rootVisualElement;
        root.Clear();
        var waitingRoot = waitingScreen.CloneTree();
        root.Add(waitingRoot);

        var characterSelectionButton = waitingRoot.Q<Button>("CharacterSelectionButton");
        var gameStartButton = waitingRoot.Q<Button>("GameStartButton");
        var optionsButton = waitingRoot.Q<Button>("OptionsButton");

        characterSelectionButton.clicked += ShowCharacterSelection;
        gameStartButton.clicked += StartGame;
        optionsButton.clicked += ShowOptions;
    }

    public void Hide()
    {
        uiDocument.rootVisualElement.style.display = DisplayStyle.None;
    }

    private void ShowCharacterSelection()
    {
        Debug.Log("캐릭터 선택 로직을 구현하세요.");
    }

    private void StartGame()
    {
        Debug.Log("게임 시작 로직을 구현하세요.");
    }

    private void ShowOptions()
    {
        Debug.Log("옵션 설정 화면 로직을 구현하세요.");
    }
}
