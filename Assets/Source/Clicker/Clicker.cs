using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Clicker : BaseInitializable
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private Button _mainButton;
    [SerializeField] private int _perClick;
    private static int _score; // TODO: Верно ли?

    public override void Initialize()
    {
        _mainButton.onClick.AddListener(() => AddScore(_perClick));
        UpdateTextValue();
    }

    private void AddScore(int value)
    {
        _score += value;
        UpdateTextValue();
    }

    private void UpdateTextValue()
    {
        _scoreText.text = "Score: " + _score;
    }
}
