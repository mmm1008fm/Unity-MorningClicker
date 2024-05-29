using UnityEngine;
using System.Collections;

public class Windmill : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(AddWindmillPowerToScore());
    }

    private IEnumerator AddWindmillPowerToScore()
    {
        while (Application.isPlaying)
        {
            yield return new WaitForSeconds(1);
            ResourceBank.Instance.Score += (int)(ResourceBank.Instance.ScorePerSecond * ResourceBank.Instance.PerSecondMultiplayer);
        }
    }
}