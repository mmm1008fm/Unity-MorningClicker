using UnityEngine;
using UnityEngine.UI;

public class MainSceneView : BaseInitializable
{
    [SerializeField] private Button _toCastle;
    [SerializeField] private Button _toShop;
    [SerializeField] private Button _toWorld;
    [SerializeField] private Button _toSettings;

    public override void Initialize()
    {
        _toCastle.onClick.AddListener(ToCastle);
        _toShop.onClick.AddListener(ToShop);
        _toWorld.onClick.AddListener(ToWorld);
        _toSettings.onClick.AddListener(ToSettings);
    }

    private void ToCastle() => SceneSwitcher.Switch("Castle");
    
    private void ToShop() => SceneSwitcher.Switch("Shop");
    
    private void ToWorld() => SceneSwitcher.Switch("World");

    private void ToSettings()
    {
        Debug.Log("Button settings");
        SceneSwitcher.Switch("Settings");
    }
}
