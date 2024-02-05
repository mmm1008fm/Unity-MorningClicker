using System;
using UnityEngine;
using UnityEngine.UI;

public class CastleSceneView : BaseInitializable
{
    [SerializeField] private Button _exit;
    [SerializeField] private Button _talk;

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
        throw new Exception("Dialogs have not yet been implemented");
    }
}
