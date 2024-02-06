using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Clicker : BaseInitializable
{
    [SerializeField] private string _prefix = "Score: ";
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private Button _mainButton;

    public override void Initialize()
    {
        _mainButton.onClick.AddListener(AddScore);
        UpdateTextValue();
    }

    private void AddScore()
    {
        PlayerVariables.Score += PlayerVariables.PerClick;
        UpdateTextValue();
    }

    private void UpdateTextValue()
    {
        _scoreText.text = _prefix + PlayerVariables.Score;
    }
}
