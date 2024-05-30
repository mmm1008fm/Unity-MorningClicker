using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class VariantButton : MonoBehaviour
{
    public static string MyText { get; set; }
    [SerializeField] private Button _button;

    private void OnValidate()
    {
        _button = GetComponent<Button>();
    }

    private void Awake()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        MyText = _button.GetComponentInChildren<TMP_Text>().text;
    }
}