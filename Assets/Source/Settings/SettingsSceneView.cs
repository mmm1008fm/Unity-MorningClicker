using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsSceneView : MonoBehaviour
{
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _soundSlider;
    [SerializeField] private Toggle _toggle;

    private void Awake()
    {
        _musicSlider.onValueChanged.AddListener(MusicVolumeChange);
        _soundSlider.onValueChanged.AddListener(SoundVolumeChange);
    }

    private void Start()
    {
        UpdateSliders();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }

        if (_toggle.isOn)
        {
            ResourceBank.Instance.ВесёлыйРежим = true;
        }
        else
        {
            ResourceBank.Instance.ВесёлыйРежим = false;
        }
    }

    private void UpdateSliders()
    {
        // Устанавливаем значения слайдеров только при включении или старте
        _musicSlider.value = ResourceBank.Instance.MusicVolume;
        _soundSlider.value = ResourceBank.Instance.SoundVolume;
    }

    private void SoundVolumeChange(float value)
    {
        ResourceBank.Instance.SoundVolume = value;
    }

    private void MusicVolumeChange(float value)
    {
        ResourceBank.Instance.MusicVolume = value;
    }
}