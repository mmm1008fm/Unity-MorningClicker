using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleResultWindow : MonoBehaviour
{
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private Button _continueButton;
    [SerializeField] private GameObject _artefactObject;
    [SerializeField] private string _continueSceneName = "WorldMap";

    private void Awake()
    {
        _artefactObject.SetActive(false);
        gameObject.SetActive(false);
        _continueButton.onClick.AddListener(Continue);
    }

    public void SetWin(BattleArtefact winArtefact, string description = "Вы одержали победу над этим врагом. Поздравляю!")
    {
        _title.text = "Победа!";
        _description.text = description;
        _artefactObject.GetComponent<Image>().sprite = winArtefact.Item;
        gameObject.SetActive(true);
        _artefactObject.SetActive(true);

        switch (winArtefact.Name)
        {
            case "Шляпа пугала":
                ResourceBank.Instance.ScarecrowHat = true;
                break;
            case "Грааль из божественного металла":
                ResourceBank.Instance.HolyCup = true;
                break;
            case "Кусок вулканической породы, все еще горячий":
                ResourceBank.Instance.LavaStoneArtefact = true;
                break;
            case "Сердце леса":
                ResourceBank.Instance.HeartOfForestArtefact = true;
                break;
            case "Свиток запретной магии":
                ResourceBank.Instance.MagicScrollArtefact = true;
                break;
            default:
                throw new System.Exception("Неизвестный артефакт. Срочно измените проверку по названием выше");
                // Название должно совпадать с названиями в ScriptableObject
        }

        ResourceBank.Instance.Save(ResourceBank.Instance);
    }

    public void SetLose(string description = "Вы потерпели поражение. Попробуйте еще раз.")
    {
        _title.text = "Поражение";
        _description.text = description;
        gameObject.SetActive(true);
    }

    private void Continue()
    {
        SceneManager.LoadScene(_continueSceneName);
    }
}