using UnityEngine;
using UnityEngine.UI;

public class ShopSceneView : BaseInitializable
{
    [SerializeField] private Button _exit;
    [SerializeField] private Button _closeWindowForBuy;
    [SerializeField] private Button _openWarriors;
    [SerializeField] private Button _openArmor;
    [SerializeField] private Button _openWindmill;
    [SerializeField] private GameObject _windowForBuy;
    [SerializeField] private GameObject _forWarriors;
    [SerializeField] private GameObject _forArmor;
    [SerializeField] private GameObject _forWindmill;

    public override void Initialize()
    {
        _exit.onClick.AddListener(Exit);
        _closeWindowForBuy.onClick.AddListener(CloseWindowForBuy);
        
        _openArmor.onClick.AddListener(OpenArmor);
        _openWarriors.onClick.AddListener(OpenWarriors);
        _openWindmill.onClick.AddListener(OpenWindmill);
    }

    private void Exit() => SceneSwitcher.Switch("Main");
    
    private void OpenWarriors()
    {
        _windowForBuy.SetActive(true);
        _forWarriors.SetActive(true);
    }
    
    private void OpenArmor()
    {
        _windowForBuy.SetActive(true);
        _forArmor.SetActive(true);
    }

    private void OpenWindmill()
    {
        _windowForBuy.SetActive(true);
        _forWindmill.SetActive(true);
    }

    private void CloseWindowForBuy()
    {
        _windowForBuy.SetActive(false);
        _forArmor.SetActive(false);
        _forWarriors.SetActive(false);
        _forWindmill.SetActive(false);
    }
}
