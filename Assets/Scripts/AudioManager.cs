using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Musica")]
    public AudioClip levelMusic;
    public AudioClip menuMusic;

    [Header("SFX")]
    public AudioClip jumpClip;
    public AudioClip stompClip;
    public AudioClip powerUpClip;
    public AudioClip damageClip;
    public AudioClip deathClip;
    public AudioClip coinClip;

    private AudioSource musicSource;
    private AudioSource sfxSource;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;
        musicSource.volume = 0.5f;

        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.volume = 1f;
    }

    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }

    public void PlayLevelMusic()
    {
        if (levelMusic == null) return;
        musicSource.clip = levelMusic;
        musicSource.Play();
    }

    public void PlayMenuMusic()
    {
        if (menuMusic == null) return;
        musicSource.clip = menuMusic;
        musicSource.Play();
    }

    public void StopMusic() => musicSource.Stop();

    public void PlayJump()    => PlaySFX(jumpClip);
    public void PlayStomp()   => PlaySFX(stompClip);
    public void PlayPowerUp() => PlaySFX(powerUpClip);
    public void PlayDamage()  => PlaySFX(damageClip);
    public void PlayDeath()   => PlaySFX(deathClip);
    public void PlayCoin()    => PlaySFX(coinClip);

    private void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;
        sfxSource.PlayOneShot(clip);
    }
}
