using TMPro;
using UnityEngine;

public class PreBattleStatistic : MonoBehaviour
{
    [SerializeField] private TMP_Text _armorCoundField;
    [SerializeField] private TMP_Text _warriorsCountField;

    private void OnEnable()
    {
        if (!ResourceBank.Instance)
        {
            return;
        }

        _armorCoundField.text = ResourceBank.Instance.Armor.ToString();
        _warriorsCountField.text = ResourceBank.Instance.Warriors.ToString();
    }
}
