using UnityEngine;
using System.Collections.Generic;

public class Sound : MonoBehaviour {

    AudioSource[] childAudioSources;
    public AudioSource CurrentSong;

	// Use this for initialization
	void Start () {
        childAudioSources = GetComponentsInChildren<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (CurrentSong == null || !CurrentSong.isPlaying)
        {
            PlayMusic();
        }
	}

    void PlayMusic()
    {
        CurrentSong = childAudioSources.SelectRandom();
        CurrentSong.Play();
    }
}
