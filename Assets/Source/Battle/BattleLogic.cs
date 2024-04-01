using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

public class BattleLogic : MonoBehaviour
{
    [SerializeField] private TMP_Text _playerPowerTextField;
    [SerializeField] private TMP_Text _enemyPowerTextField;
    [SerializeField] private Button _attackButton;
    [SerializeField] private List<GameObject> _hidingObjectsOnAttack;

    private int _playerPower;
    private int _enemyPower;

    private void Awake()
    {
        _attackButton.onClick.AddListener(OnClickToAttack);
    }

    private void Start()
    {
        _enemyPower = PlayerVariables.Warriors + Random.Range(-3, 5);
        _playerPower = PlayerVariables.Warriors;
    }

    private void Update()
    {
        _playerPowerTextField.text = $"Союзные силы:\n{_playerPower} едениц";
        _enemyPowerTextField.text = $"Силы противника:\n{_enemyPower} едениц";
    }

    private async void OnClickToAttack()
    {

        foreach (var obj in _hidingObjectsOnAttack)
        {
            obj.SetActive(false);
        }

        AttackResult r = await UniTask.Create(AttackToWin);

        switch (r)
        {
            case AttackResult.Win:
                Debug.Log("Победа!");
                break;
            case AttackResult.Deffeat:
                Debug.Log("Поражение!");
                break;
            case AttackResult.Nobody:
                Debug.Log("Ничья!");
                break;
        }

        foreach (var obj in _hidingObjectsOnAttack)
        {
            obj.SetActive(true);
        }
    }

    private async UniTask<AttackResult> AttackToWin()
    {
        if (_playerPower <= 0)
        {
            Debug.Log("Недостаточно юнитов");
            return AttackResult.Nobody;
        }

        while (_playerPower > 0 && _enemyPower > 0)
        {
            await UniTask.Delay(Random.Range(750, 1500));

            _playerPower -= 1;
            _enemyPower -= 1;

            if (_playerPower <= 0)
            {
                _playerPower = 0;
                return AttackResult.Deffeat;
            }

            if (_enemyPower <= 0)
            {
                _enemyPower = 0;
                return AttackResult.Win;
            }
        }

        return AttackResult.Nobody;
    }

    private enum AttackResult
    {
        Win, Deffeat, Nobody
    }
}
