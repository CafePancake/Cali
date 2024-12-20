using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "SoundBank", menuName = "Create SoundBank")]
public class SOSounds : ScriptableObject
{
    [SerializeField] AudioClip _casinoJackpot;
    public AudioClip casinoJackpot=>_casinoJackpot;
}
