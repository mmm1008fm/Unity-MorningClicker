using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Clicker : MonoBehaviour, IPointerDownHandler
{
    public static event UnityAction OnClick;

    public void OnPointerDown(PointerEventData eventData)
    {
        OnClick.Invoke();
        AddScore();
    }

    private void AddScore()
    {
        ResourceBank.Instance.Score += ResourceBank.Instance.ScorePerClick;
    }
}
