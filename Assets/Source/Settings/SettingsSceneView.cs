using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsSceneView : MonoBehaviour
{
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _soundSlider;

    private void Awake()
    {
        _musicSlider.onValueChanged.AddListener(MusicVolumeChange);
        _soundSlider.onValueChanged.AddListener(SoundVolumeChange);
    }

    private void Update()
    {
        UpdateSliders();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    private void OnEnable()
    {
        UpdateSliders();
    }

    private void Start()
    {
        UpdateSliders();
    }

    private void UpdateSliders()
    {
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