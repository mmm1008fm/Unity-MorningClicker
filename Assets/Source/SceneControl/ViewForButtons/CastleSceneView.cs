using UnityEngine;
using UnityEngine.UI;

public class CastleSceneView : BaseInitializable
{
    [SerializeField] private Button _exit;
    [SerializeField] private Button _talk;
    [SerializeField] private Dialogue _dialogue;
    [SerializeField] private DialogueObject _setDialogue;

    public override void Initialize()
    {
        _exit.onClick.AddListener(Exit);
        _talk.onClick.AddListener(Talk);
    }

    private void Exit()
    {
        SceneSwitcher.Switch("Main");
    }
    
    private void Talk()
    {
        _dialogue.StartDialogue(_setDialogue);
    }
}
