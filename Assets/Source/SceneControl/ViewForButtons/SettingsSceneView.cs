using UnityEngine;
using UnityEngine.UI;

public class SettingsSceneView : BaseInitializable
{
    [SerializeField] private Button _back;

    public override void Initialize()
    {
        _back.onClick.AddListener(ToMain);
    }

    private void ToMain()
    {
        SceneSwitcher.Switch("Main");
    }
}
