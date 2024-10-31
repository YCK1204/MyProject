using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx : IManager
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
        SceneManager.LoadScene(sceneName);
    }
    public int GetSceneCounts()
    {
        return SceneManager.sceneCount;
    }
}
