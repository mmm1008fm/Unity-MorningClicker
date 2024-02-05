using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private List<BaseInitializable> _initializables;

    private void Awake()
    {
        foreach (var baseInitializable in _initializables)
        {
            baseInitializable.Initialize();
        }
    }
}
