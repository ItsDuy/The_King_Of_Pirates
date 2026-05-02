using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    private const string MusicVolumeKey = "Audio_MusicVolume";
    private const string SfxVolumeKey = "Audio_SfxVolume";
    private const string MusicEnabledKey = "Audio_MusicEnabled";
    private const string SfxEnabledKey = "Audio_SfxEnabled";

    [Header("Default Settings")]
    [SerializeField, Range(0f, 1f)] private float defaultMusicVolume = 1f;
    [SerializeField, Range(0f, 1f)] private float defaultSfxVolume = 1f;
    [SerializeField] private bool musicEnabled = true;
    [SerializeField] private bool sfxEnabled = true;

    public float MusicVolume { get; private set; }
    public float SfxVolume { get; private set; }

    private readonly List<AudioSource> musicSources = new List<AudioSource>();
    private readonly List<AudioSource> sfxSources = new List<AudioSource>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadSettings();
        RefreshAllSources();
    }

    public void RegisterMusicSource(AudioSource audioSource)
    {
        if (audioSource == null || musicSources.Contains(audioSource))
        {
            return;
        }

        musicSources.Add(audioSource);
        ApplyMusicSource(audioSource);
    }

    public void UnregisterMusicSource(AudioSource audioSource)
    {
        if (audioSource == null)
        {
            return;
        }

        musicSources.Remove(audioSource);
    }

    public void RegisterSfxSource(AudioSource audioSource)
    {
        if (audioSource == null || sfxSources.Contains(audioSource))
        {
            return;
        }

        sfxSources.Add(audioSource);
        ApplySfxSource(audioSource);
    }

    public void UnregisterSfxSource(AudioSource audioSource)
    {
        if (audioSource == null)
        {
            return;
        }

        sfxSources.Remove(audioSource);
    }

    public void SetMusicVolume(float volume)
    {
        MusicVolume = Mathf.Clamp01(volume);
        SaveSettings();
        RefreshMusicSources();
    }

    public void SetSfxVolume(float volume)
    {
        SfxVolume = Mathf.Clamp01(volume);
        SaveSettings();
        RefreshSfxSources();
    }

    public void SetMusicEnabled(bool enabled)
    {
        musicEnabled = enabled;
        SaveSettings();
        RefreshMusicSources();
    }

    public void SetSfxEnabled(bool enabled)
    {
        sfxEnabled = enabled;
        SaveSettings();
        RefreshSfxSources();
    }

    public void ToggleMusic()
    {
        SetMusicEnabled(!musicEnabled);
    }

    public void ToggleSfx()
    {
        SetSfxEnabled(!sfxEnabled);
    }

    private void RefreshAllSources()
    {
        RefreshMusicSources();
        RefreshSfxSources();
    }

    private void RefreshMusicSources()
    {
        for (int index = musicSources.Count - 1; index >= 0; index--)
        {
            AudioSource audioSource = musicSources[index];

            if (audioSource == null)
            {
                musicSources.RemoveAt(index);
                continue;
            }

            ApplyMusicSource(audioSource);
        }
    }

    private void RefreshSfxSources()
    {
        for (int index = sfxSources.Count - 1; index >= 0; index--)
        {
            AudioSource audioSource = sfxSources[index];

            if (audioSource == null)
            {
                sfxSources.RemoveAt(index);
                continue;
            }

            ApplySfxSource(audioSource);
        }
    }

    private void ApplyMusicSource(AudioSource audioSource)
    {
        audioSource.volume = musicEnabled ? MusicVolume : 0f;
    }

    private void ApplySfxSource(AudioSource audioSource)
    {
        audioSource.volume = sfxEnabled ? SfxVolume : 0f;
    }

    private void LoadSettings()
    {
        MusicVolume = PlayerPrefs.GetFloat(MusicVolumeKey, defaultMusicVolume);
        SfxVolume = PlayerPrefs.GetFloat(SfxVolumeKey, defaultSfxVolume);
        musicEnabled = PlayerPrefs.GetInt(MusicEnabledKey, musicEnabled ? 1 : 0) == 1;
        sfxEnabled = PlayerPrefs.GetInt(SfxEnabledKey, sfxEnabled ? 1 : 0) == 1;
    }

    private void SaveSettings()
    {
        PlayerPrefs.SetFloat(MusicVolumeKey, MusicVolume);
        PlayerPrefs.SetFloat(SfxVolumeKey, SfxVolume);
        PlayerPrefs.SetInt(MusicEnabledKey, musicEnabled ? 1 : 0);
        PlayerPrefs.SetInt(SfxEnabledKey, sfxEnabled ? 1 : 0);
        PlayerPrefs.Save();
    }
}