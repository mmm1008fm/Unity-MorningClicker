using UnityEngine;

public class SystemsController : MonoBehaviour
{
    private static SystemsController singleton;
    
    private void Awake()
    {
        if (!singleton)
        {
            DontDestroyOnLoad(gameObject);
            singleton = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
