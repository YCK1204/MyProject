using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Loader : MonoBehaviour
{
    private static Loader instance;
    private static string targetScene;

    public static void Load(string scene)
    {
        targetScene = scene;
        SceneManager.LoadScene("loading-menu");

        // �ε� ��ũ��Ʈ ������ ���� Loader ������Ʈ ����
        GameObject loaderObject = new GameObject("Loader");
        instance = loaderObject.AddComponent<Loader>();
    }

    private void Start()
    {
        StartCoroutine(LoadSceneAsync());
    }

    private IEnumerator LoadSceneAsync()
    {
        yield return new WaitForSeconds(1f); // �ε� ȭ���� ���� ���̵��� ������ �߰�

        AsyncOperation operation = SceneManager.LoadSceneAsync(targetScene);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            LoaderManager.UpdateLoadingProgress(progress);

            if (operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true;
            }
            yield return null;
        }

        Destroy(gameObject); // �� ��ȯ �� Loader ������Ʈ ����
    }
}
