using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SoundCenter : MonoBehaviour
{
    public Slider bgmSlider;
    public Slider sfxSlider;

    private float[] volumeLevels = { 0f, 0.2f, 0.4f, 0.6f, 0.8f, 1f };

    private void Start() 
    {
        InitValue();
    }

    public void InitValue()
    {
        bgmSlider.value = SoundManager.Instance.GetBGMVolume();
        sfxSlider.value = SoundManager.Instance.GetSFXVolume();
    }

    public void ChangeBGMVolume(float value)
    {
        float volume = SetVolumeValue(bgmSlider);
        SoundManager.Instance.ChangeBGMVolume(volume);

        bgmSlider.value = volume;
    }


    public void ChangeSFXVolume(float value)
    {
        float volume = SetVolumeValue(sfxSlider);
        SoundManager.Instance.ChangeSFXVolume(volume);
        
        sfxSlider.value = volume;
    }

    private float SetVolumeValue(Slider slider)
    {
        float value = slider.value;
        float nearestValue = volumeLevels[0];

        foreach (float level in volumeLevels)
        {
            if (Mathf.Abs(value - level) < Mathf.Abs(value - nearestValue))
            {
                nearestValue = level;
            }
        }
        return nearestValue;
    }
}
