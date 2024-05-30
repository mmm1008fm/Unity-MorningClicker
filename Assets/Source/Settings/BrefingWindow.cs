using TMPro;
using UnityEngine;

public class BrefingWindow : MonoBehaviour
{
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private GameObject _canBattle;
    [SerializeField] private GameObject _canNotBattle;

    private void Awake()
    {
        gameObject.SetActive(false);

        _canBattle.SetActive(false);
        _canNotBattle.SetActive(false);
    }

    private void OnEnable()
    {
        var info = BrefingTransfer.Info;

        if (info == null)
        {
            return;
        }

        _title.text = info.Location.Name;
        _description.text = info.Location.Description;

        switch (info.Location.Name)
        {
            case "Лес злобных друидов":
                if (ResourceBank.Instance.HeartOfForestArtefact)
                {
                    _canBattle.SetActive(true);
                    _canNotBattle.SetActive(false);
                }
                else
                {
                    _canBattle.SetActive(false);
                    _canNotBattle.SetActive(true);
                }
                break;
            case "Проклятое пшеничное поле":
                if (ResourceBank.Instance.ScarecrowHat)
                {
                    _canBattle.SetActive(true);
                    _canNotBattle.SetActive(false);
                }
                else
                {
                    _canBattle.SetActive(false);
                    _canNotBattle.SetActive(true);
                }
                break;
            case "Остров магии огня со злобнымыи лавовыми големами":
                if (ResourceBank.Instance.LavaStoneArtefact)
                {
                    _canBattle.SetActive(true);
                    _canNotBattle.SetActive(false);
                }
                else
                {
                    _canBattle.SetActive(false);
                    _canNotBattle.SetActive(true);
                }
                break;
            case "Остров преверженцев темной магии":
                if (ResourceBank.Instance.MagicScrollArtefact)
                {
                    _canBattle.SetActive(true);
                    _canNotBattle.SetActive(false);
                }
                else
                {
                    _canBattle.SetActive(false);
                    _canNotBattle.SetActive(true);
                }
                break;
            case "Царство ангелов, повязших в грехах":
                if (ResourceBank.Instance.HolyCup)
                {
                    _canBattle.SetActive(true);
                    _canNotBattle.SetActive(false);
                }
                else
                {
                    _canBattle.SetActive(false);
                    _canNotBattle.SetActive(true);
                }
                break;
        }
    }
}
