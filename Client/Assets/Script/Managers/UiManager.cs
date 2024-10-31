using UnityEngine;

public class UIManager : MonoBehaviour, IManager
{
    public GameObject mainMenu;
    public GameObject gameUI;
    public GameObject gameOverMenu;

    public void Init()
    {
        // 초기 UI 설정
        ShowMainMenu();
    }

    public void Clear()
    {
        // UI 관련 리소스 정리 로직 (필요시)
    }

    public void ShowMainMenu()
    {
        mainMenu.SetActive(true);
        gameUI.SetActive(false);
        gameOverMenu.SetActive(false);
    }

    public void ShowGameUI()
    {
        mainMenu.SetActive(false);
        gameUI.SetActive(true);
        gameOverMenu.SetActive(false);
    }

    public void ShowGameOverMenu()
    {
        mainMenu.SetActive(false);
        gameUI.SetActive(false);
        gameOverMenu.SetActive(true);
    }
}
