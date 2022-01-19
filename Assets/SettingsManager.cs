using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI musicSlider;
    [SerializeField] private TextMeshProUGUI soundSlider;
    [SerializeField] private SoundSettings soundSettings;

    private void Start()
    {
        UpdateSoundSlider(soundSettings.SoundVolume);
        UpdateMusicSlider(soundSettings.MusicVolume);
    }
    
    private void UpdateSoundSlider(int charactersAmount)
    {
        var volumeText = "";
        for (var i = 0; i < charactersAmount; i++)
        {
            volumeText += "| ";
        }
        soundSlider.text = volumeText;
    }
    
    private void UpdateMusicSlider(int charactersAmount)
    {
        var volumeText = "";
        for (var i = 0; i < charactersAmount; i++)
        {
            volumeText += "| ";
        }
        musicSlider.text = volumeText;
    }
    
    public void IncreaseSoundVolume()
    {
       
        soundSettings.SoundVolume += 1;
        var charactersAmount = (int) (soundSettings.SoundVolume);
        UpdateSoundSlider(charactersAmount);
    } 
    
    public void DecreaseSoundVolume()
    {
        soundSettings.SoundVolume -= 1;
        var charactersAmount = soundSettings.SoundVolume ;
        UpdateSoundSlider(charactersAmount);
    } 
    
    public void IncreaseMusicVolume()
    {
        soundSettings.MusicVolume += 1;
        var charactersAmount = soundSettings.MusicVolume;
        UpdateMusicSlider(charactersAmount);
    } 
    
    public void DecreaseMusicVolume()
    {
        soundSettings.MusicVolume -= 1;
        var charactersAmount = soundSettings.MusicVolume;
        UpdateMusicSlider(charactersAmount);
    }

    public void BackToMenu()
    {
        GameFlowManager.Instance.LoadMenu();
    }

}
