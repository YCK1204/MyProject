using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    [Header("Background Music")]
    [SerializeField] private int bgMusicIndex; // 배열에서 사용할 인덱스

    private void Start()
    {
        // SoundManager의 인스턴스를 통해 배경 음악 재생
        SoundManager.Instance.PlayBackgroundMusic(bgMusicIndex);
    }
}
