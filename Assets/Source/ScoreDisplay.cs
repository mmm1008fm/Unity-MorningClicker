using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private string _prefix = "Score: ";
    [SerializeField] private TextMeshProUGUI _scoreText;

    private void OnEnable()
    {
        PlayerVariables.Singleton.OnScoreChanged += UpdateScoreDisplay;
        UpdateScoreDisplay();
    }

    private void OnDisable()
    {
        PlayerVariables.Singleton.OnScoreChanged -= UpdateScoreDisplay;
    }

    private void UpdateScoreDisplay()
    {
        _scoreText.text = _prefix + PlayerVariables.Singleton.Score;
    }
}