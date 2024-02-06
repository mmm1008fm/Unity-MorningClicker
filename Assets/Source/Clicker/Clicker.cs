using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Clicker : BaseInitializable
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private Button _mainButton;
    [SerializeField] private int _perClick;

    public override void Initialize()
    {
        _mainButton.onClick.AddListener(() => AddScore(_perClick));
        UpdateTextValue();
    }

    private void AddScore(int value)
    {
        PlayerVariables.Score += value;
        UpdateTextValue();
    }

    private void UpdateTextValue()
    {
        _scoreText.text = "Score: " + PlayerVariables.Score;
    }
}
