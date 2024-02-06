using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSceneView : BaseInitializable
{
    [SerializeField] private Button _exit;
    [SerializeField] private List<Button> _buyButtons;
    [SerializeField] private Button _closeWindowForBuy;
    [SerializeField] private GameObject _windowForBuy;

    public override void Initialize()
    {
        _exit.onClick.AddListener(Exit);
        _closeWindowForBuy.onClick.AddListener(CloseWindowForBuy);
        foreach (var buyButton in _buyButtons)
        {
            var buttonName = buyButton.name; // TODO: Верно ли?
            buyButton.onClick.AddListener(() => Buy(buttonName)); // TODO: Верно ли?
        }
    }

    private void Exit()
    {
        SceneSwitcher.Switch("Main");
    }
    
    private void Buy(string buttonName) // TODO: Верно ли?
    {
        _windowForBuy.SetActive(true);
    }

    private void CloseWindowForBuy()
    {
        _windowForBuy.SetActive(false);
    }
}
