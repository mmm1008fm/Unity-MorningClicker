using UnityEngine;

public class CursorMover : MonoBehaviour
{
    [SerializeField] private RectTransform[] _buttons;
    [SerializeField] private Transform _targetObject; // Объект, который нужно перемещать

    private void Update()
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
            _targetObject.position = closestButton.position;
        }
    }
}