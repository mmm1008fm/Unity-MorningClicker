using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Clicker : BaseInitializable
{
    [SerializeField] private Button _mainButton;
    [SerializeField] private TextMeshProUGUI _score;

    public override void Initialize()
    {
        _mainButton.onClick.AddListener(AddScore);
    }

    private void AddScore()
    {
        PlayerVariables.Score += PlayerVariables.PerClick;
        _score.text = $"Score: {PlayerVariables.Score}";
    }
}
