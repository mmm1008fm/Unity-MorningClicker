using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    private void OnEnable()
    {
        PlayerVariables.OnScoreChanged += UpdateScoreDisplay;
        UpdateScoreDisplay();
    }

    private void OnDisable()
    {
        PlayerVariables.OnScoreChanged -= UpdateScoreDisplay;
    }

    private void UpdateScoreDisplay()
    {
        _scoreText.text = $"Score: {PlayerVariables.Score}";
    }
}