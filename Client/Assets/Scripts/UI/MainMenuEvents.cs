using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuEvents : MonoBehaviour
{
    private UIDocument _document;
    private Button _startButton;
    private AudioSource _audioSource;

    private void Awake()
    {
        _document = GetComponent<UIDocument>();
        if (_document == null)
        {
            Debug.LogError("UIDocument를 찾을 수 없습니다!");
            return;
        }

        VisualElement root = _document.rootVisualElement;

        _audioSource = GetComponent<AudioSource>();

        _startButton = root.Q<Button>("StartGameButton");
        if (_startButton != null)
        {
            _startButton.clicked += StartGame;
        }
        else
        {
            Debug.LogError("StartGameButton을 찾을 수 없습니다!");
        }

        root.Query<Button>().ForEach(button =>
        {
            button.clicked += () => _audioSource?.Play();
        });
    }

    private void OnDisable()
    {
        if (_startButton != null)
        {
            _startButton.clicked -= StartGame;
        }
    }

    private void StartGame()
    {
        Debug.Log("게임 시작 버튼 클릭됨!");
        Loader.Load("Level-1");
    }
}
