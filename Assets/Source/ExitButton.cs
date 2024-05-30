using UnityEngine;

public class ExitButton : MonoBehaviour
{
    private void Start()
    {
        #if PLATFORM_WEBGL
            gameObject.SetActive(false);
        #else
            gameObject.SetActive(true);
        #endif
    }
}