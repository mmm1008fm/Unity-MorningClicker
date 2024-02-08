using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private string _prefix = "Score: ";
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
        Debug.Log("Обновление текста на экране");
        _scoreText.text = _prefix + PlayerVariables.Score;
    }
}