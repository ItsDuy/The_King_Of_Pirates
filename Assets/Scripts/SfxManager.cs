using UnityEngine;

public class SfxManager : MonoBehaviour
{
    [SerializeField] private AudioSource sfxSource;

    private void Start()
    {
        if (sfxSource == null)
        {
            sfxSource = GetComponent<AudioSource>();
        }

        if (sfxSource == null)
        {
            return;
        }

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.RegisterSfxSource(sfxSource);
        }
    }

    private void OnDestroy()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.UnregisterSfxSource(sfxSource);
        }
    }

    public void Play(AudioClip clip)
    {
        if (sfxSource == null || clip == null)
        {
            return;
        }

        sfxSource.PlayOneShot(clip);
    }
}