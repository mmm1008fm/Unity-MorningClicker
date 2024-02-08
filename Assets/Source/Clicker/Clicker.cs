using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Clicker : MonoBehaviour, IPointerDownHandler
{
    public static event UnityAction OnClick;

    /*public override void Initialize()
    {
        _mainButton.onClick.AddListener(AddScore);
    }*/

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("Нажатие на кнопку");
        OnClick.Invoke();
        AddScore();
    }

    private void AddScore()
    {
        PlayerVariables.Score += PlayerVariables.PerClick;
        // _score.text = $"Score: {PlayerVariables.Score}";
    }
}
