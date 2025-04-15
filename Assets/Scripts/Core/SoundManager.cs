using UnityEngine;

public sealed class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _effectsSource;
    [SerializeField] private AudioSource _musicSource;
    
    public float LowPitchRange = .95f;
    public float HighPitchRange = 1.05f;
    
    public static SoundManager Instance = null;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad (gameObject);
    }
    
    public void Play(AudioClip clip)
    {
        _effectsSource.clip = clip;
        _effectsSource.Play();
    }
    
    public void PlayMusic(AudioClip clip)
    {
        _musicSource.clip = clip;
        _musicSource.Play();
    }
    
    public void RandomSoundEffect(params AudioClip[] clips)
    {
        var randomIndex = Random.Range(0, clips.Length);
        var randomPitch = Random.Range(LowPitchRange, HighPitchRange);

        _effectsSource.pitch = randomPitch;
        _effectsSource.clip = clips[randomIndex];
        _effectsSource.Play();
    }
	
}