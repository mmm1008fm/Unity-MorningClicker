using TMPro;
using UnityEngine;

public class BrefingWindow : MonoBehaviour
{
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _description;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        var info = BrefingTransfer.Info;

        if (info == null)
        {
            return;
        }

        _title.text = info.Location.Name;
        _description.text = info.Location.Description;
    }
}
