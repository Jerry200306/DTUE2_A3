using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio; 

public class VolumeControl : MonoBehaviour
{
    public Slider volumeSlider; 
    public AudioSource backgroundMusic;

    void Start()
    {

        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 0.8f);
        backgroundMusic.volume = volumeSlider.value;


        volumeSlider.onValueChanged.AddListener(ChangeVolume);
    }

    public void ChangeVolume(float volume)
    {
        backgroundMusic.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume);
    }
}