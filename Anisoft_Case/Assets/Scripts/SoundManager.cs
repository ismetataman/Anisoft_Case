using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    static AudioSource audioSource;
    public static AudioClip droppingSound, mixerSound, pouringSound, waterShakingSound, winSound;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        droppingSound = Resources.Load<AudioClip>("Sounds/Dropping");
        mixerSound = Resources.Load<AudioClip>("Sounds/Mixer");
        winSound = Resources.Load<AudioClip>("Sounds/Win");
        pouringSound = Resources.Load<AudioClip>("Sounds/Pouring");
        waterShakingSound = Resources.Load<AudioClip>("Sounds/Water Shaking");
    }

    public static void DropSound()
    {
        audioSource.PlayOneShot(droppingSound,1f);
    }
    public static void MixerSound()
    {
        audioSource.PlayOneShot(mixerSound, 1f);
    }
    public static void PourSound()
    {
        audioSource.PlayOneShot(pouringSound,1f);
    }
    public static void WaterShakeSound()
    {
        audioSource.PlayOneShot(waterShakingSound, 1f);
    }
    public static void WinSound()
    {
        audioSource.PlayOneShot(winSound, 1f);
    }



}
