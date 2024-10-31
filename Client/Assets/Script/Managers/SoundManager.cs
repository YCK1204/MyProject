using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource backgroundMusicSource;
    [SerializeField] private AudioSource soundEffectSource;

    [Header("Background Music Clips")]
    [SerializeField] private AudioClip[] backgroundMusicClips; // 배경 음악 배열

    [Header("Sound Effect Clips")]
    [SerializeField] private AudioClip footstepSound; // 발자국 소리
    [SerializeField] private AudioClip[] soundEffects; // 효과음 배열

    private Dictionary<string, AudioClip> soundEffectDictionary;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 간 유지
        }
        else
        {
            Destroy(gameObject);
        }

        // 효과음 사전 초기화
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
