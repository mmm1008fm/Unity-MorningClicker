using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSceneView : BaseInitializable
{
    [SerializeField] private Button _exit;
    [SerializeField] private List<Button> _buyButtons;

    public override void Initialize()
    {
        _exit.onClick.AddListener(Exit);
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
        throw new Exception($"Shop have not yet been implemented (Button: {buttonName})");
    }
}
