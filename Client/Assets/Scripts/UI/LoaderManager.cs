using UnityEngine;

public static class LoaderManager
{
    private static float loadingProgress = 0.0f;

    public static void UpdateLoadingProgress(float progress)
    {
        loadingProgress = progress;
    }

    public static float GetLoadingProgress()
    {
        return loadingProgress;
    }
}
