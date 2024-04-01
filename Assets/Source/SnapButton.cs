using NaughtyAttributes;
using UnityEngine;
using UnityEngine.EventSystems;

public class SnapButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] ButtonAction _action;
    [SerializeField, ShowIf("_action", ButtonAction.SwitchScene)] string _sceneName;

    public void OnPointerDown(PointerEventData eventData)
    {
        switch (_action)
        {
            case ButtonAction.SwitchScene:
                SceneSwitcher.Switch(_sceneName);
                break;
            case ButtonAction.ReloadScene:
                SceneSwitcher.Reload();
                break;
            case ButtonAction.QuitGame:
                SceneSwitcher.QuitGame();
                break;
        }
    }

    private enum ButtonAction
    {
        SwitchScene,
        ReloadScene,
        QuitGame
    }
}
