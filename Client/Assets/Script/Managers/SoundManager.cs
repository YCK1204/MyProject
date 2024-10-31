using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource backgroundMusicSource;
    [SerializeField] private AudioSource soundEffectSource;

    [Header("Background Music Clips")]
    [SerializeField] private AudioClip[] backgroundMusicClips; // ��� ���� �迭

    [Header("Sound Effect Clips")]
    [SerializeField] private AudioClip footstepSound; // ���ڱ� �Ҹ�
    [SerializeField] private AudioClip[] soundEffects; // ȿ���� �迭

    private Dictionary<string, AudioClip> soundEffectDictionary;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� �� ����
        }
        else
        {
            Destroy(gameObject);
        }

        // ȿ���� ���� �ʱ�ȭ
        soundEffectDictionary = new Dictionary<string, AudioClip>();
        for (int i = 0; i < soundEffects.Length; i++)
        {
            soundEffectDictionary.Add($"Effect_{i}", soundEffects[i]);
        }
    }

    public void PlayBackgroundMusic(int clipIndex)
    {
        if (clipIndex >= 0 && clipIndex < backgroundMusicClips.Length)
        {
            backgroundMusicSource.clip = backgroundMusicClips[clipIndex];
            backgroundMusicSource.Play();
        }
    }

    public void PlayFootstepSound()
    {
        soundEffectSource.PlayOneShot(footstepSound);
    }

    public void PlaySoundEffect(string effectName)
    {
        if (soundEffectDictionary.TryGetValue(effectName, out AudioClip clip))
        {
            soundEffectSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"Sound effect '{effectName}' not found!");
        }
    }
}
