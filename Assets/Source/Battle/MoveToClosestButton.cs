using UnityEngine;
using DG.Tweening;

public class MoveToClosestButton : MonoBehaviour
{
    [SerializeField] private RectTransform[] _buttons;
    [SerializeField] private Transform targetObject; // Объект, который нужно перемещать
    [SerializeField] private float moveDuration = 0.5f; // Время на перемещение
    [SerializeField] private Vector3 onLocationScale = new Vector3(0.8f, 0.8f, 0.8f); // Размер при нахождении на кнопке
    [SerializeField] private Vector3 offLocationScale = new Vector3(1f, 1f, 1f); // Размер при нахождении вне кнопки
    [SerializeField] private Vector3 movingScale = new Vector3(1.2f, 1.2f, 1.2f); // Размер при движении

    private bool isMoving = false;
    private Vector3 targetPosition;
    private Tween moveTween;
    private Tween scaleTween;

    private void Update()
    {
        if (_buttons.Length == 0 || targetObject == null) return;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0; // Убедитесь, что z координата равна 0, если вы работаете в 2D

        float minDistance = float.MaxValue;
        RectTransform closestButton = null;

        foreach (var button in _buttons)
        {
            if (button == null) continue; // Skip null buttons

            float distance = Vector3.Distance(mousePos, button.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestButton = button;
            }
        }

        if (closestButton != null && closestButton.gameObject.activeInHierarchy)
        {
            // Если объект уже движется к той же кнопке, ничего не делаем
            if (isMoving && targetPosition == closestButton.position)
            {
                return;
            }

            // Обновляем целевую позицию
            targetPosition = closestButton.position;

            // Отменяем текущие твины, если они существуют
            moveTween?.Kill();
            scaleTween?.Kill();

            try
            {
                // Устанавливаем размер объекта в размер для движения
                scaleTween = targetObject.DOScale(movingScale, moveDuration);
            }
            catch { }

            try
            {
                // Плавное перемещение объекта к ближайшей кнопке
                moveTween = targetObject.DOMove(targetPosition, moveDuration).OnComplete(() =>
                {
                    // Изменение размера объекта в зависимости от его нахождения на кнопке
                    scaleTween = targetObject.DOScale(onLocationScale, moveDuration);
                    isMoving = false;
                });
                isMoving = true;
            }
            catch { }
        }
        else
        {
            // Если нет ближайшей кнопки или объект находится вне кнопки
            if (isMoving)
            {
                isMoving = false;
            }

            // Отменяем текущие твины, если они существуют
            moveTween?.Kill();
            scaleTween?.Kill();

            // Изменение размера объекта при нахождении вне кнопки
            scaleTween = targetObject.DOScale(offLocationScale, moveDuration);
        }
    }

    private void OnDisable()
    {
        // Отменяем все активные твины при отключении объекта
        moveTween?.Kill();
        scaleTween?.Kill();
    }

    private void OnDestroy()
    {
        // Отменяем все активные твины при уничтожении объекта
        moveTween?.Kill();
        scaleTween?.Kill();
    }
}