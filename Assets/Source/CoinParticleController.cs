using UnityEngine;

public class CoinParticleController : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSystemPrefab;

    public void SpawnParticle()
    {
        ParticleSystem particleSystemInstance = Instantiate(particleSystemPrefab, transform);
        Destroy(particleSystemInstance.gameObject, particleSystemInstance.main.duration);
    }
}
