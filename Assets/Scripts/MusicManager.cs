using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private Dropdown musicDropdown; // Reference to the Dropdown UI for music selection (only used in settings)

    [Header("Music Settings")]
    [SerializeField] private AudioClip[] musicTracks; // Array of possible music tracks
    [SerializeField] private AudioSource backgroundMusicSource; // Assign your main background AudioSource here

    private static MusicManager instance;

    private void Awake()
    {
        // Singleton pattern to ensure only one instance exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Keep music manager persistent between scenes
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // If backgroundMusicSource is not assigned, add an AudioSource dynamically
        if (backgroundMusicSource == null)
        {
            backgroundMusicSource = gameObject.AddComponent<AudioSource>();
            backgroundMusicSource.loop = true;
            backgroundMusicSource.playOnAwake = false;
        }
    }

    private void Start()
    {
        // Load saved music track or use default (index 0)
        int savedTrackIndex = PlayerPrefs.GetInt("MusicTrack", 0);
        PlayMusic(savedTrackIndex);

        // If dropdown is present (in settings), sync it with saved music and add listener
        if (musicDropdown != null)
        {
            musicDropdown.value = savedTrackIndex;
            musicDropdown.onValueChanged.AddListener(ChangeMusic);
        }
    }

    // Method to play music by index
    private void PlayMusic(int index)
    {
        if (index >= 0 && index < musicTracks.Length)
        {
            backgroundMusicSource.clip = musicTracks[index];
            backgroundMusicSource.Play();
        }
        else
        {
            Debug.LogWarning("Music track index out of range!");
        }
    }

    // Called when dropdown changes
    public void ChangeMusic(int index)
    {
        PlayMusic(index);
        PlayerPrefs.SetInt("MusicTrack", index);  // Save the selected track
        PlayerPrefs.Save();  // Ensure the change is saved immediately
    }

    private void OnDestroy()
    {
        // Clean up listener when the object is destroyed
        if (musicDropdown != null)
        {
            musicDropdown.onValueChanged.RemoveListener(ChangeMusic);
        }
    }
}