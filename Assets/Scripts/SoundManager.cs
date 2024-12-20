using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField] AudioSource _audio;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);

        _audio = GetComponent<AudioSource>();
    }

    public void PlayClip(AudioClip clip)
    {
        _audio.PlayOneShot(clip);
    }
}
