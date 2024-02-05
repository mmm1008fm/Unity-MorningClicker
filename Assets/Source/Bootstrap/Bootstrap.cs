using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private Clicker _clicker;

    private void Awake()
    {
        _clicker.Initialize();
    }
}
