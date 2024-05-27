using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class TalkButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _statusTextField;
    [SerializeField] private Dialogue _dialogueSystem;
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
            _statusTextField.text = "Разговаривать";
        }
    }

    private void OnClickToTalk()
    {
        if (_dialogueTasks.Count == 0 || !_dialogueTasks[0].Initialized)
        {
            return;
        }

        _dialogueTasks[0].Activate();
        _dialogueSystem.StartDialogue(_dialogueTasks[0].DialogueObject);
        _dialogueTasks.Remove(_dialogueTasks[0]);
    }
}