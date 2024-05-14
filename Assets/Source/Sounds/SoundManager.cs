using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private List<SoundSetupPair> _sounds;
    [SerializeField] private AudioSource _audioSource;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Play(string soundName)
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
            return;
        }

        _audioSource.volume = pair.Volume * ResourceBank.Instance.SoundVolume;
        _audioSource.PlayOneShot(pair.Sound[Random.Range(0, pair.Sound.Count)]);
    }
}