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

        // 로딩 스크립트 실행을 위해 Loader 오브젝트 생성
        GameObject loaderObject = new GameObject("Loader");
        instance = loaderObject.AddComponent<Loader>();
    }

    private void Start()
    {
        StartCoroutine(LoadSceneAsync());
    }

    private IEnumerator LoadSceneAsync()
    {
        yield return new WaitForSeconds(1f); // 로딩 화면이 먼저 보이도록 딜레이 추가

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

        Destroy(gameObject); // 씬 전환 후 Loader 오브젝트 삭제
    }
}
