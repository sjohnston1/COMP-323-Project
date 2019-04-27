using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeChange : MonoBehaviour
{
    //reference to audio source component
    private AudioSource audioSrc;

    //The defualt volume sound. Should be 1 to match up with slider in unity
    private float musicVolume = 1f;

    // Start is called before the first frame update
    void Start()
    {
        //assign the audio source component to control it
        audioSrc = GetComponent<AudioSource>();    
    }

    // Update is called once per frame
    void Update()
    {
        //setting the volume option of the audio source to be equal to the music volume
        audioSrc.volume = musicVolume;
    }

    //called by the slider game object. Takes volume value passed by the slider and sets it as the music value
    public void SetVolume(float vol) {
        musicVolume = vol;
    }
}
