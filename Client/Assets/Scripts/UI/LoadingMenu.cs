using UnityEngine;
using UnityEngine.UIElements;

public class LoadingMenu : MonoBehaviour
{
    private ProgressBar progressBar;

    private void Awake()
    {
        var rootElement = GetComponent<UIDocument>()?.rootVisualElement;
        if (rootElement == null)
        {
            Debug.LogError("UIDocument�� ã�� �� �����ϴ�!");
            return;
        }

        progressBar = rootElement.Q<ProgressBar>("loading-progress-bar");
    }

    private void Update()
    {
        if (progressBar != null)
        {
            progressBar.value = LoaderManager.GetLoadingProgress();
        }
    }
}
