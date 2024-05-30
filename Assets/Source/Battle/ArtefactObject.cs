using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ArtefactObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [field: SerializeField] public BattleArtefact ArtefactDefinition { get; set; }
    [SerializeField] private GameObject _tooltipPrefab;
    [SerializeField] private float _tooltipDelay = 0.5f;
    [SerializeField] private Transform _hintTransform;

    private GameObject _tooltipInstance;
    private Coroutine _showTooltipCoroutine;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _showTooltipCoroutine = StartCoroutine(ShowTooltipWithDelay());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_showTooltipCoroutine != null)
        {
            StopCoroutine(_showTooltipCoroutine);
            _showTooltipCoroutine = null;
        }

        if (_tooltipInstance != null)
        {
            Destroy(_tooltipInstance);
            _tooltipInstance = null;
        }
    }

    private IEnumerator ShowTooltipWithDelay()
    {
        yield return new WaitForSeconds(_tooltipDelay);

        _tooltipInstance = Instantiate(_tooltipPrefab, transform.position + Vector3.up * 1, Quaternion.identity, _hintTransform);
        UpdateTooltipContent();
    }

    private void UpdateTooltipContent()
    {
        if (_tooltipInstance != null)
        {
            var nameText = _tooltipInstance.transform.Find("NameText").GetComponent<TMP_Text>();
            var descriptionText = _tooltipInstance.transform.Find("DescriptionText").GetComponent<TMP_Text>();
            var itemImage = _tooltipInstance.transform.Find("ItemImage").GetComponent<Image>();

            nameText.text = ArtefactDefinition.Name;
            descriptionText.text = ArtefactDefinition.Description;
            itemImage.sprite = ArtefactDefinition.Item;
        }
    }
}