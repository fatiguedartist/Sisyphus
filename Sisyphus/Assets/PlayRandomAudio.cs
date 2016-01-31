using UnityEngine;
using System.Collections.Generic;

public class PlayRandomAudio : MonoBehaviour {

    public List<AudioClip> audioClips;
    public AudioSource audioSource;
	
	// Update is called once per frame
	void Update () {
	    if (!audioSource.isPlaying)
	    {
	        audioSource.PlayOneShot(audioClips.SelectRandom());
	    }
	}
}
