using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private List<SoundSetupPair> _sounds;
    [SerializeField] private int _audioSourcePoolSize = 10;
    private List<AudioSource> _audioSourcePool;
    private int _currentAudioSourceIndex = 0;
    private bool _firstStart;
    private AudioSource _musicSource;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeAudioSourcePool();
            _firstStart = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (_firstStart)
        {
            var a = Instance.PlayMusic("bgmusic");
            _firstStart = false;
            _musicSource = a;
        }
    }

    private void Update()
    {
        _musicSource.volume = ResourceBank.Instance.MusicVolume;
    }

    private void InitializeAudioSourcePool()
    {
        _audioSourcePool = new List<AudioSource>();
        for (int i = 0; i < _audioSourcePoolSize; i++)
        {
            var audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.loop = false;
            audioSource.playOnAwake = false;
            _audioSourcePool.Add(audioSource);
        }
    }

    public AudioSource Play(string soundName)
    {
        SoundSetupPair pair = null;

        foreach (var sound in _sounds)
        {
            if (sound.Key == soundName)
            {
                pair = sound;
                break;
            }
        }

        if (pair == null)
        {
            Debug.LogWarning("Не найден звук c названием: " + soundName + " в листе " + _sounds);
            return null;
        }

        var audioSource = GetAvailableAudioSource();
        audioSource.volume = pair.Volume * ResourceBank.Instance.SoundVolume;
        audioSource.PlayOneShot(pair.Sound[Random.Range(0, pair.Sound.Count)]);
        return audioSource;
    }

    public AudioSource PlayMusic(string soundName)
    {
        SoundSetupPair pair = null;

        foreach (var sound in _sounds)
        {
            if (sound.Key == soundName)
            {
                pair = sound;
                break;
            }
        }

        if (pair == null)
        {
            Debug.LogWarning("Не найден звук c названием: " + soundName + " в листе " + _sounds);
            return null;
        }

        var audioSource = GetAvailableAudioSource();
        audioSource.clip = pair.Sound[Random.Range(0, pair.Sound.Count)];
        audioSource.volume = pair.Volume * ResourceBank.Instance.MusicVolume;
        audioSource.loop = true; // Включаем зацикливание
        audioSource.Play();
        return audioSource;
    }

    private AudioSource GetAvailableAudioSource()
    {
        var audioSource = _audioSourcePool[_currentAudioSourceIndex];
        _currentAudioSourceIndex = (_currentAudioSourceIndex + 1) % _audioSourcePoolSize;
        return audioSource;
    }
}