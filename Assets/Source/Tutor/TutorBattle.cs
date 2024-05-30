using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class TutorBattle : MonoBehaviour
{
    public bool BattleEnd;
    [SerializeField] private Image _progressBar;
    [SerializeField] private Image _rageBar;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _rageBar.fillAmount += 0.04f;
        }

        _rageBar.fillAmount = Mathf.Clamp(_rageBar.fillAmount - 0.2f * Time.deltaTime, 0f, 1f);
    }

    public async UniTask StartBattle()
    {
        while (_progressBar.fillAmount > 0f)
        {
            _progressBar.fillAmount -= 0.045f;
            await UniTask.Delay(1000 - (int)(_rageBar.fillAmount * 500));
        }

        BattleEnd = true;
    }
}
