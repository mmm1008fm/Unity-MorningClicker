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