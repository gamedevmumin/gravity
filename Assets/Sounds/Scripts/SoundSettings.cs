using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu]
public class SoundSettings : ScriptableObject
{
    private int musicVolume = 10;

    private int soundVolume = 10;

    public int MusicVolume
    {
        get => musicVolume;
        set
        {
            musicVolume = value;
            if (musicVolume >= 10)
            {
                musicVolume = 10;
            } 
            else if (musicVolume <= 0)
            {
                musicVolume = 0;
            }
        }
    }
    
    public int SoundVolume
    {
        get => soundVolume;
        set
        {
            soundVolume = value;
            if (soundVolume > 10)
            {
                soundVolume = 10;
            } 
            else if (soundVolume < 0)
            {
                soundVolume = 0;
            }
        }
    }
}
