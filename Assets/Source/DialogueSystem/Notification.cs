using UnityEngine;

public class Notification : MonoBehaviour
{
    // Use only start or update
    public static GameObject Instance { get; private set; }

    private void Awake()
    {
        Instance = gameObject;
    }
}