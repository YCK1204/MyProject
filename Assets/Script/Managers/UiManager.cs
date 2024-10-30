using UnityEngine;

public class UIManager : MonoBehaviour, IManager
{
    public GameObject mainMenu;
    public GameObject gameUI;
    public GameObject gameOverMenu;

    public void Init()
    {
        // �ʱ� UI ����
        ShowMainMenu();
    }

    public void Clear()
    {
        // UI ���� ���ҽ� ���� ���� (�ʿ��)
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
