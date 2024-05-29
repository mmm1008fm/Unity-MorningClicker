using UnityEngine;

public class ArtefactViewer : MonoBehaviour
{
    [SerializeField] private ArtefactDefinition _artefact;

    private void Start()
    {
        switch (_artefact)
        {
            case ArtefactDefinition.MagicScroll:
                if (ResourceBank.Instance.MagicScrollArtefact)
                {
                    gameObject.SetActive(true);
                }
                else
                {
                    gameObject.SetActive(false);
                }
                break;
            case ArtefactDefinition.LavaStone:
                if (ResourceBank.Instance.LavaStoneArtefact)
                {
                    gameObject.SetActive(true);
                }
                else
                {
                    gameObject.SetActive(false);
                }
                break;
            case ArtefactDefinition.HeartOfForest:
                if (ResourceBank.Instance.HeartOfForestArtefact)
                {
                    gameObject.SetActive(true);
                }
                else
                {
                    gameObject.SetActive(false);
                }
                break;
            case ArtefactDefinition.ScarecrowHat:
                if (ResourceBank.Instance.ScarecrowHat)
                {
                    gameObject.SetActive(true);
                }
                else
                {
                    gameObject.SetActive(false);
                }
                break;
            case ArtefactDefinition.HolyCup:
                if (ResourceBank.Instance.HolyCup)
                {
                    gameObject.SetActive(true);
                }
                else
                {
                    gameObject.SetActive(false);
                }
                break;
        }
    }

    private enum ArtefactDefinition
    {
        MagicScroll,
        LavaStone,
        HeartOfForest,
        ScarecrowHat,
        HolyCup
    }
}