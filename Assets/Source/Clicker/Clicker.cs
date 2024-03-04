using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Clicker : MonoBehaviour, IPointerDownHandler
{
    public static event UnityAction OnClick;
    private PlayerVariables _playerVariables;

    private void Awake() => _playerVariables = new PlayerVariables();

    public void OnPointerDown(PointerEventData eventData)
    {
        OnClick?.Invoke();
        AddScore(_playerVariables.PerClick);
    }

    private void AddScore(int value) => _playerVariables.Score += value;
}
