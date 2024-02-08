using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class CoinParticleController : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystemPrefab;
    [SerializeField] private List<AudioSource> _sounds;
    [SerializeField] private UIlayer _layer;
    private float _posZ;

    private void OnEnable()
    {
        Clicker.OnClick += SpawnParticle;
    }

    private void OnDisable()
    {
        Clicker.OnClick -= SpawnParticle;
    }

    private void SpawnParticle()
    {
        switch (_layer)
        {
            case UIlayer.Front:
                _posZ = -60f;
                break;
            case UIlayer.Back:
                _posZ = 40f;
                break;
            case UIlayer.Center:
                _posZ = -10f;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        _sounds[Random.Range(0, _sounds.Count)].Play();
        var worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var particleSystemInstance = Instantiate(
            _particleSystemPrefab, new Vector3(worldPos.x, worldPos.y, _posZ), quaternion.identity);
        particleSystemInstance.Play();
        Destroy(particleSystemInstance.gameObject, particleSystemInstance.main.duration);
    }
    
    private enum UIlayer
    {
        Front,
        Back,
        Center
    }
}
