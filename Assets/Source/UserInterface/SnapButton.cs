using NaughtyAttributes;
using UnityEngine;
using UnityEngine.EventSystems;

public class SnapButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] ButtonAction _action;
    [SerializeField, ShowIf("_action", ButtonAction.SwitchScene)] string _sceneName;
    [SerializeField, ShowIf("_action", ButtonAction.OpenURL)] string _url;
    [SerializeField, ShowIf("_action", ButtonAction.BattleScene)] BrefingInfo _brefingInfo;

    public void OnPointerDown(PointerEventData eventData)
    {
        SoundManager.Instance.Play("btn_click");

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
            case ButtonAction.ResetProgress:
                ResourceBank.Instance.Reset();
                break;
            case ButtonAction.Nothing:
                break;
            case ButtonAction.OpenURL:
                if (!string.IsNullOrEmpty(_url))
                {
                    Application.OpenURL(_url);
                }
                break;
            case ButtonAction.BattleScene:
                BrefingTransfer.Info = _brefingInfo;
                SceneSwitcher.Switch("Battle");
                break;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        SoundManager.Instance.Play("btn_up");
    }

    private enum ButtonAction
    {
        Nothing,
        SwitchScene,
        ReloadScene,
        QuitGame,
        ResetProgress,
        OpenURL,
        BattleScene
    }
}
