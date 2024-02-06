using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CastleSceneView : BaseInitializable
{
    public Button _talk;
    [SerializeField] private Button _exit;
    [SerializeField] private Dialogue _dialogue;
    [SerializeField] private DialogueObject _setDialogue;

    public override void Initialize()
    {
        _exit.onClick.AddListener(Exit);
        _talk.onClick.AddListener(Talk);
        Dialogue.onClose += EndTalk; // TODO: Фигня?
    }

    private void Exit()
    {
        SceneSwitcher.Switch("Main");
    }
    
    private void Talk()
    {
        _dialogue.StartDialogue(_setDialogue);
        _talk.gameObject.SetActive(false);
    }

    private void EndTalk()
    {
        _talk.gameObject.SetActive(true);
    }
}
