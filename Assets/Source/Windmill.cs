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
            PlayerVariables.Singleton.Score += PlayerVariables.Singleton.WindmillPower;
        }
    }
}

