using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;

    [SerializeField] private bool playOnStart = true;

    private void Start()
    {
        if (musicSource == null)
        {
            musicSource = GetComponent<AudioSource>();
        }

        if (musicSource == null)
        {
            return;
        }

        musicSource.loop = true;

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.RegisterMusicSource(musicSource);
        }

        if (playOnStart && !musicSource.isPlaying)
        {
            musicSource.Play();
        }
    }

    private void OnDestroy()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.UnregisterMusicSource(musicSource);
        }
    }
}
