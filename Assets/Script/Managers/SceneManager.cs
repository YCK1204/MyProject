using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour, IManager
{
    public void Init()
    {
        Debug.Log("SceneManager initialized");
        // 필요한 씬 로딩 및 설정
    }

    public void Clear()
    {
        Debug.Log("SceneManager cleared");
        // 씬 관련 리소스 정리 로직
    }

    public void LoadScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
