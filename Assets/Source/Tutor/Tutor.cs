using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Tutor : MonoBehaviour
{
    public bool IsWarriorsBought;
    [SerializeField] private Canvas _outCastle;
    [SerializeField] private Canvas _inCastle;
    [SerializeField] private Canvas _shop;
    [SerializeField] private Dialogue _dialogueSystem;
    [SerializeField] private GameObject _dialogueBody;
    [SerializeField] private DialogueObject _firstAct;
    [SerializeField] private DialogueObject _secondAct;
    [SerializeField] private DialogueObject _thirdAct;
    [SerializeField] private DialogueObject _shopAct;
    [SerializeField] private DialogueObject _afterBuy;
    [SerializeField] private Button _shopButton;
    [SerializeField] private GameObject _shopTask;
    [SerializeField] private GameObject _warriors;
    [SerializeField] private GameObject _castle;
    [SerializeField] private GameObject _shopWindow;
    private bool _isShop;

    private void Awake()
    {
        _outCastle.gameObject.SetActive(false);
        _inCastle.gameObject.SetActive(false);
        _shop.gameObject.SetActive(false);
        _dialogueBody.SetActive(false);
        _shopButton.gameObject.SetActive(false);
        _shopButton.onClick.AddListener(OpenShop);
        _shopTask.SetActive(false);
        _warriors.SetActive(false);
        _castle.SetActive(false);
        _shopWindow.SetActive(false);
    }

    private async void Start()
    {
        _inCastle.gameObject.SetActive(true);
        await _dialogueSystem.StartDialogue(_firstAct);
        _inCastle.gameObject.SetActive(false);
        _outCastle.gameObject.SetActive(true);
        await _dialogueSystem.StartDialogue(_secondAct);
        _outCastle.gameObject.GetComponentInChildren<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1f);
        _castle.SetActive(true);
        await UniTask.WaitWhile(() => ResourceBank.Instance.Score < 50);
        _outCastle.gameObject.GetComponentInChildren<Image>().color = new Color(1f, 1f, 1f, 1f);
        _castle.SetActive(false);
        await _dialogueSystem.StartDialogue(_thirdAct);
        _shopButton.gameObject.SetActive(true);
        await UniTask.WaitWhile(() => !_isShop);
        await _dialogueSystem.StartDialogue(_shopAct);
        _shopTask.SetActive(true);
        _warriors.SetActive(true);
        _outCastle.gameObject.SetActive(false);
        await UniTask.WaitWhile(() => !IsWarriorsBought);
        await _dialogueSystem.StartDialogue(_afterBuy);
        ResourceBank.Instance.FirstTime = false;
    }

    private void OpenShop()
    {
        _shop.gameObject.SetActive(true);
        _inCastle.gameObject.SetActive(false);
        _castle.gameObject.SetActive(false);
        _outCastle.gameObject.SetActive(false);
        _isShop = true;
    }
}