using System;
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

    private async void OnClickToTalk()
    {
        if (_dialogueTasks.Count == 0 || !_dialogueTasks[0].Initialized)
        {
            return;
        }

        _button.interactable = false;
        await TalkEvent(_dialogueTasks[0].Context[0], _dialogueTasks);
        _button.interactable = true;
    }

    private async UniTask TalkEvent(DialoguesContext context, List<DialogueTask> inputTasks)
    {
        var answersCopy = new string[inputTasks[0].Answers.Length];
        var currentTask = inputTasks[0];

        for (int i = 0; i < answersCopy.Length; i++)
        {
            answersCopy[i] = inputTasks[0].Answers[i];
        }

        await _dialogueSystem.StartDialogue(context.Start);

        // Логика выбора вариантов
        GameObject[] variant = new GameObject[context.Answers.Length];
        VariantButton[] variantButton = new VariantButton[context.Answers.Length];

        for (int i = 0; i < context.Answers.Length; i++)
        {
            variant[i] = Instantiate(_variantPrefab, _variantsParent);
            variant[i].GetComponentInChildren<TMP_Text>().text = answersCopy[i];
            variantButton[i] = variant[i].GetComponent<VariantButton>();
        }

        await UniTask.WaitWhile(() => string.IsNullOrEmpty(VariantButton.MyText));

        for (int i = 0; i < context.Answers.Length; i++)
        {
            Destroy(variant[i]);
        }

        int numOfContext = -1;

        numOfContext = VariantButton.MyText switch
        {
            "Принять" => 0,
            "Отказать" => 1,
            _ => throw new ArgumentException(nameof(VariantButton.MyText)),
        };

        VariantButton.MyText = string.Empty;

        // Продолжение диалога
        await _dialogueSystem.StartDialogue(context.Answers[numOfContext]);
        await _dialogueSystem.StartDialogue(context.End);

        if (numOfContext == 0)
        {
            currentTask.Activate();
        }
    }
}