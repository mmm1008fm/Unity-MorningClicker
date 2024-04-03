using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public static UnityAction onClose;
    [SerializeField] private Image PersonAvatar;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _content;
    [SerializeField] private GameObject _dialogueBody;
    [SerializeField] private float _textSpeed;
    
    private int _index;
    private DialogueObject _dialogue;

    private void Update()
    {
        CheckDialogue();
    }

    private void CheckDialogue()
    {
        if (!Input.GetMouseButtonDown(0))
        {
            return;
        }
        
        if (!_dialogue)
        {
            return;
        }
        
        if (_content.text == _dialogue.Content[_index])
        {
            NextLine();
        }
        else
        {
            StopAllCoroutines();
            _content.text = _dialogue.Content[_index];
        }
    }

    public void StartDialogue(DialogueObject dialogue)
    {
        if (_dialogueBody.activeSelf)
        {
            return;
        }

        _dialogue = dialogue;
        _index = 0;
        _content.text = string.Empty;
        PersonAvatar.sprite = _dialogue.PersonAwatar;
        _name.text = _dialogue.PersonName;
        _dialogueBody.SetActive(true);
        StartCoroutine(TypeLine());
    }
    
    private IEnumerator TypeLine()
    {
        foreach (var c in _dialogue.Content[_index].ToCharArray())
        {
            _content.text += c;
            yield return new WaitForSeconds(_textSpeed);
        }
    }
    
    private void NextLine()
    {
        if (_index < _dialogue.Content.Length - 1)
        {
            _index++;
            _content.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            _dialogue = null;
            _dialogueBody.SetActive(false);
            onClose?.Invoke();
        }
    }
}