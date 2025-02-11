using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider; // Reference to the Slider in the UI

    private void Start()
    {
        // Set the slider's value based on the current volume of the AudioListener
        volumeSlider.value = AudioListener.volume;

        // Add a listener to change the volume whenever the slider value changes
        volumeSlider.onValueChanged.AddListener(UpdateVolume);
    }

    // This method is called when the slider value changes
    public void UpdateVolume(float volume)
    {
        AudioListener.volume = volume; // Change the global audio volume (affects all AudioSources)
    }

    private void OnDestroy()
    {
        // Remove the listener when the object is destroyed to prevent memory leaks
        volumeSlider.onValueChanged.RemoveListener(UpdateVolume);
    }
}