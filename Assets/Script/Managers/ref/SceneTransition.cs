using System.Collections;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad; // �ε��� �� �̸�

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
        // GameManager�� ���� ���� �ε�
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
