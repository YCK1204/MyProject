using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour, IManager
{
    public void Init()
    {
        Debug.Log("SceneManager initialized");
        // �ʿ��� �� �ε� �� ����
    }

    public void Clear()
    {
        Debug.Log("SceneManager cleared");
        // �� ���� ���ҽ� ���� ����
    }

    public void LoadScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
