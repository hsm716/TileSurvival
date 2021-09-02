using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource sound;
    void Awake()
    {
        sound = GetComponent<AudioSource>();
        sound.Play();
    }

}
