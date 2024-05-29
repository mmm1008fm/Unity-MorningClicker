using UnityEngine;
using UnityEngine.UI;

public class Closet : MonoBehaviour
{
    [SerializeField] private Sprite _normal;
    [SerializeField] private Sprite _old;
    [SerializeField] private Image _image;

    private void Start()
    {
        var r = ResourceBank.Instance;

        if (r.MagicScrollArtefact || r.LavaStoneArtefact || r.HeartOfForestArtefact || r.ScarecrowHat || r.HolyCup)
        {
            _image.sprite = _normal;
        }
        else
        {
            _image.sprite = _old;
        }
    }
}