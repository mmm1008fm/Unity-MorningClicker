using UnityEngine;

public class Player : MonoBehaviour
{
    private GameObject _notificationObject;
    private DialogueCycle _dialogueCycle;

    private void Start()
    {
        _notificationObject = GameObject.Find("DialogueNotification");
        _dialogueCycle = new DialogueCycle();
        _dialogueCycle.StartCycle().Forget();
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        for (int i = 0; i < _dialogueCycle.DialogueTasks.Count; i++)
        {
            if (!_dialogueCycle.DialogueTasks[i].Initialized)
            {
                _dialogueCycle.DialogueTasks.Remove(_dialogueCycle.DialogueTasks[i]);
            }
        }

        if (_dialogueCycle.DialogueTasks.Count == 0)
        {
            _notificationObject.SetActive(false);
        }
        else
        {
            _notificationObject.SetActive(true);
        }
    }
}