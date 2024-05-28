using UnityEngine;
using DG.Tweening;

public class MoveToClosestButton : MonoBehaviour
{
    [SerializeField] private RectTransform[] _buttons;
    [SerializeField] private Transform targetObject; // Объект, который нужно перемещать
    [SerializeField] private float moveDuration = 0.5f; // Время на перемещение
    [SerializeField] private Vector3 onLocationScale = new Vector3(0.8f, 0.8f, 0.8f); // Размер при нахождении на кнопке
    [SerializeField] private Vector3 offLocationScale = new Vector3(1f, 1f, 1f); // Размер при нахождении вне кнопки

    void Update()
    {
        if (_buttons.Length == 0) return;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0; // Убедитесь, что z координата равна 0, если вы работаете в 2D

        float minDistance = float.MaxValue;
        RectTransform closestButton = null;

        foreach (var button in _buttons)
        {
            float distance = Vector3.Distance(mousePos, button.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestButton = button;
            }
        }

        if (closestButton != null)
        {
            // Плавное перемещение объекта к ближайшей кнопке
            targetObject.DOMove(closestButton.position, moveDuration);

            // Изменение размера объекта в зависимости от его нахождения на кнопке
            targetObject.DOScale(onLocationScale, moveDuration);
        }
        else
        {
            // Изменение размера объекта при нахождении вне кнопки
            targetObject.DOScale(offLocationScale, moveDuration);
        }
    }
}