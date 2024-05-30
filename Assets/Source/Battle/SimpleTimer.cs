using UnityEngine;
using TMPro;
using Cysharp.Threading.Tasks;

[RequireComponent(typeof(TMP_Text))]
public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private BattleLogic battleLogic;

    private void Start()
    {
        StartTimer().Forget();
    }

    private async UniTaskVoid StartTimer()
    {
        float startTime = Time.time;
        
        while (true)
        {
            float elapsedTime = Time.time - startTime;
            int seconds = Mathf.FloorToInt(elapsedTime);
            int milliseconds = Mathf.FloorToInt((elapsedTime - seconds) * 1000);

            if (battleLogic.IsEnd)
            {
                return;
            }

            timerText.text = $"{seconds:D2}:{milliseconds/10:D2}";

            await UniTask.Yield();

            if (battleLogic.IsEnd)
            {
                return;
            }
        }
    }
}