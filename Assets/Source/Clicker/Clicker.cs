using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Clicker : BaseInitializable
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private Button _mainButton;
    [SerializeField] private int _perClick;
    private int _score;

    public override void Initialize()
    {
        _mainButton.onClick.AddListener(() => AddScore(_perClick));
    }

    private void AddScore(int value)
    {
        _score += value;
        _scoreText.text = "Score: " + _score;
    }
}
