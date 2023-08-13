using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSetting : MonoBehaviour
{

    public Slider slider;
    public Text volumeAmount;

    private void Start()
    {
        LoadAudio();
    }

    public void SetAudio(float value)
    {
        AudioListener.volume = value;
        volumeAmount.text = ((int)(value * 100)).ToString();
        SaveAudio();
    }

    private void SaveAudio()
    {
        PlayerPrefs.SetFloat("audiovolume", AudioListener.volume);
    }

    private void LoadAudio()
    {

        if (PlayerPrefs.HasKey("audiovolume"))
        {
            AudioListener.volume = PlayerPrefs.GetFloat("audiovolume");
            slider.value = PlayerPrefs.GetFloat("audiovolume");
        }
        else
        {
            PlayerPrefs.SetFloat("audiovolume", 0.8f);
            AudioListener.volume = PlayerPrefs.GetFloat("audiovolume");
            slider.value = PlayerPrefs.GetFloat("audiovolume");
        }

    }
}
