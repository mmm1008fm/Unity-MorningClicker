using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class TalkButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _statusTextField;
    [SerializeField] private Dialogue _dialogueSystem;
    [SerializeField] private GameObject _variantPrefab;
    [SerializeField] private Transform _variantsParent;
    private List<DialogueTask> _dialogueTasks => Player.Instance.DialogueCycle.DialogueTasks;
    private Button[] _variants;

    private void OnValidate()
    {
        _button = GetComponent<Button>();
    }

    private void Awake()
    {
        _button.onClick.AddListener(OnClickToTalk);
    }

    private void Update()
    {
        if (_dialogueTasks.Count == 0)
        {
            _statusTextField.text = "Ничего";
        }
        else
        {
            _statusTextField.text = "Спросить";
        }
    }

    private void OnClickToTalk()
    {
        if (_dialogueTasks.Count == 0 || !_dialogueTasks[0].Initialized)
        {
            return;
        }

        _button.interactable = false;
        TalkEvent().Forget();
    }

    private async UniTaskVoid TalkEvent()
    {
        GameObject[] buttons = new GameObject[_dialogueTasks[0].Answers.Length];
        await _dialogueSystem.StartDialogue(_dialogueTasks[0].DialogueObjects[0]);

        GameObject[] variant = new GameObject[_dialogueTasks[0].Answers.Length];
        VariantButton[] variantButton = new VariantButton[_dialogueTasks[0].Answers.Length];

        for (int i = 0; i < _dialogueTasks[0].Answers.Length; i++)
        {
            variant[i] = Instantiate(_variantPrefab, _variantsParent);
            variant[i].GetComponentInChildren<TMP_Text>().text = _dialogueTasks[0].Answers[i];
            buttons[i] = variant[i];
            variantButton[i] = variant[i].GetComponent<VariantButton>();
        }

        await UniTask.WaitWhile(() => string.IsNullOrEmpty(VariantButton.MyText));

        for (int i = 0; i < _dialogueTasks[0].Answers.Length; i++)
        {
            Destroy(buttons[i]);
        }

        await _dialogueSystem.StartDialogue(_dialogueTasks[0].DialogueObjects[1]);
        await _dialogueSystem.StartDialogue(_dialogueTasks[0].DialogueObjects[2]);
        _dialogueTasks[0].Activate();
        _dialogueTasks.Remove(_dialogueTasks[0]);
    }
}