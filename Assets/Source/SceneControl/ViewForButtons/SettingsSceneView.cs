using UnityEngine;
using UnityEngine.UI;

public class SettingsSceneView : BaseInitializable
{
    [SerializeField] private Button _back;
    [SerializeField] private Slider _volume;
    [SerializeField] private Slider _music;

    public override void Initialize()
    {
        _back.onClick.AddListener(ToMain);
        _volume.value = PlayerVariables.GameVolume;
        _music.value = PlayerVariables.MusicVolume;
        _volume.onValueChanged.AddListener(ChangeGameVolume);
        _music.onValueChanged.AddListener(ChangeMusicVolume);
    }

    private void ToMain() => SceneSwitcher.Switch("Main");

    private void ChangeGameVolume(float value) => PlayerVariables.GameVolume = value;
    
    private void ChangeMusicVolume(float value) => PlayerVariables.MusicVolume = value;
}
