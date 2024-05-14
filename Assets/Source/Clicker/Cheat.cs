using UnityEngine;

public class Cheat : MonoBehaviour
{
    [SerializeField] private int _score = 10000;

    private void Start()
    {
        ResourceBank.Instance.Score = _score;
    }
}
