using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor : MonoBehaviour
{
    public SongData songData;
    public AudioSource musicSource;

    public float secPerBeat;
    public float songPosition;
    public float songPositionInBeats;
    public float dspSongTime;
    

    // Start is called before the first frame update
    void Start()
    {

        //Load the AudioSource attached to the Conductor GameObject
        musicSource.clip = songData.audioClip;

        //Calculate the number of seconds in each beat
        secPerBeat = 60f / songData.bpm;

        //Record the time when the music starts
        dspSongTime = (float)AudioSettings.dspTime;

        //Start the music
        musicSource.Play();

        

    }

    // Update is called once per frame
    void Update()
    {
        //determine how many seconds since the song started
        songPosition = (float)(AudioSettings.dspTime - dspSongTime);

        //determine how many beats since the song started
        songPositionInBeats = songPosition / secPerBeat;
    }
}
