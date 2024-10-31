using System.Collections;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad; // 로드할 씬 이름

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            StartCoroutine(LoadSceneAfterDelay(4.0f));
        }
    }

    private IEnumerator LoadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        // GameManager를 통해 씬을 로드
        if (GameManager.Instance != null)
        {
            GameManager.Instance.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogError("GameManager is not available.");
        }
    }
}
