using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ButtonViewPair
{
    [field: SerializeField] public Button Button { get; private set; }
    [field: SerializeField] public GameObject GameObject { get; private set; }
}