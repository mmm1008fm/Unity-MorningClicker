using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoundSetupPair
{
    public string Key;
    public List<AudioClip> Sound;
    [Range(0, 1)] public float Volume = 1;
}