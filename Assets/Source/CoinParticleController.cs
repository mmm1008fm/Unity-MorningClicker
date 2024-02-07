using System.Collections.Generic;
using UnityEngine;

public class CoinParticleController : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystemPrefab;
    [SerializeField] private List<AudioSource> _sounds;

    public void SpawnParticle()
    {
        _sounds[Random.Range(0, _sounds.Count)].Play();
        ParticleSystem particleSystemInstance = Instantiate(_particleSystemPrefab, transform);
        particleSystemInstance.Play();
        Destroy(particleSystemInstance.gameObject, particleSystemInstance.main.duration + 10f);
    }
}
