using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSceneView : BaseInitializable
{
    [SerializeField] private List<ButtonViewPair> _buttonViewPairs;
    [SerializeField] private List<GameObject> _objectsToClose;
    
    [SerializeField] private Button _exit;
    [SerializeField] private Button _closeWindowForBuy;
    [SerializeField] private GameObject _windowForBuy;

    private bool _isOpened;

    public override void Initialize()
    {
        _exit.onClick.AddListener(Exit);
        _closeWindowForBuy.onClick.AddListener(CloseWindowForBuy);
        
        foreach (var buttonViewPair in _buttonViewPairs)
        {
            buttonViewPair.Button.onClick.AddListener(() =>
            {
                _windowForBuy.SetActive(true);

                if (!_isOpened)
                {
                    buttonViewPair.GameObject.SetActive(true);
                    _isOpened = true;
                }
            });
        }
    }

    private void Exit() => SceneSwitcher.Switch("Main");

    private void CloseWindowForBuy()
    {
        foreach (var o in _objectsToClose)
        {
            o.SetActive(false);
            _isOpened = false;
        }
    }
}
