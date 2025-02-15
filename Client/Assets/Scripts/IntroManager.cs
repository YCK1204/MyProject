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

        // VideoPlayer ����
        videoPlayer = gameObject.AddComponent<VideoPlayer>();
        videoPlayer.playOnAwake = false; // �ڵ� ��� �� ��
        videoPlayer.isLooping = false;  // ���� �ݺ� �� ��
        videoPlayer.renderMode = VideoRenderMode.APIOnly;  // ������ ȭ�鿡 ǥ��
    }

    public void Show()
    {
        var root = uiDocument.rootVisualElement;
        root.Clear();
        var introRoot = introScreen.CloneTree();
        root.Add(introRoot);

        // VideoPlayer ����
        string videoPath = "Assets/IntroVideo.mp4";  // ���� ���
        videoPlayer.url = videoPath;
        videoPlayer.Prepare();  // ���� �غ�
    }

    public void Hide()
    {
        uiDocument.rootVisualElement.style.display = DisplayStyle.None;
    }

    public void PlayIntroVideo()
    {
        videoPlayer.Play();  // ���� ���
    }
}
