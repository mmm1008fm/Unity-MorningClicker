using TMPro;
using UnityEngine;

public class BattleLogic : MonoBehaviour
{
    [SerializeField] private TMP_Text _playerPowerTextField;
    [SerializeField] private TMP_Text _enemyPowerTextField;

    private int _playerPower;
    private int _enemyPower;

    private void Awake()
    {
        _enemyPower = Random.Range(0, 100);
        _playerPower = PlayerVariables.Warriors;
        _playerPowerTextField.text = $"Союзные силы:\n{_playerPower} едениц";
        _enemyPowerTextField.text = $"Силы противника:\n{_enemyPower} едениц";
    }
}
