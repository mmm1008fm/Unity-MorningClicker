using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ButtonViewPair
{
    [field: SerializeField] public Button Button { get; private set; }
    [field: SerializeField] public GameObject GameObject { get; private set; }
    
    
}

public class ShopSceneView : BaseInitializable
{
    [SerializeField] private List<ButtonViewPair> _buttonViewPairs;
    [SerializeField] private List<GameObject> _objectsToClose;
    
    [SerializeField] private Button _exit;
    [SerializeField] private Button _closeWindowForBuy;
    [SerializeField] private GameObject _windowForBuy;

    public override void Initialize()
    {
        _exit.onClick.AddListener(Exit);
        _closeWindowForBuy.onClick.AddListener(CloseWindowForBuy);
        
        foreach (var buttonViewPair in _buttonViewPairs)
        {
            buttonViewPair.Button.onClick.AddListener(() =>
            {
                _windowForBuy.SetActive(true);
                buttonViewPair.GameObject.SetActive(true);
            });
        }
    }

    private void Exit() => SceneSwitcher.Switch("Main");

    private void CloseWindowForBuy()
    {
        foreach (var o in _objectsToClose)
        {
            o.SetActive(false);
        }
    }
}
