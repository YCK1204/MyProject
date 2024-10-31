using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    [Header("Background Music")]
    [SerializeField] private int bgMusicIndex; // �迭���� ����� �ε���

    private void Start()
    {
        // SoundManager�� �ν��Ͻ��� ���� ��� ���� ���
        SoundManager.Instance.PlayBackgroundMusic(bgMusicIndex);
    }
}
