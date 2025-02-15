using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Video;

public class IntroManager : MonoBehaviour
{
    private VisualTreeAsset introScreen;
    private UIDocument uiDocument;
    private VideoPlayer videoPlayer;

    void Awake()
    {
        uiDocument = GetComponent<UIDocument>();
        introScreen = Resources.Load<VisualTreeAsset>("IntroScreen");

        // VideoPlayer 설정
        videoPlayer = gameObject.AddComponent<VideoPlayer>();
        videoPlayer.playOnAwake = false; // 자동 재생 안 함
        videoPlayer.isLooping = false;  // 영상 반복 안 함
        videoPlayer.renderMode = VideoRenderMode.APIOnly;  // 비디오를 화면에 표시
    }

    public void Show()
    {
        var root = uiDocument.rootVisualElement;
        root.Clear();
        var introRoot = introScreen.CloneTree();
        root.Add(introRoot);

        // VideoPlayer 설정
        string videoPath = "Assets/IntroVideo.mp4";  // 영상 경로
        videoPlayer.url = videoPath;
        videoPlayer.Prepare();  // 영상 준비
    }

    public void Hide()
    {
        uiDocument.rootVisualElement.style.display = DisplayStyle.None;
    }

    public void PlayIntroVideo()
    {
        videoPlayer.Play();  // 영상 재생
    }
}
