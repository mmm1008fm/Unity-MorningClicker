using UnityEngine;

public class Cheat : MonoBehaviour
{
    [SerializeField] private ShopManager _shopManager;
    [SerializeField] private int _score = 10000;

    private void Start()
    {
        _shopManager.SetBalance(_score);
    }
}
