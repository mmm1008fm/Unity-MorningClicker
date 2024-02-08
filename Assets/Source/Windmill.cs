using UnityEngine;
using System.Collections;

public class Windmill : MonoBehaviour
{
    private void Start()
    {
        // DontDestroyOnLoad(this.gameObject);
        StartCoroutine(AddWindmillPowerToScore());
    }


    private IEnumerator AddWindmillPowerToScore()
    {
        while (Application.isPlaying)
        {
            yield return new WaitForSeconds(1);
            PlayerVariables.Score += PlayerVariables.WindmillPower;
        }
    }
}

