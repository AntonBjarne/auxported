using UnityEngine;

public class BeatManager : MonoBehaviour
{
    public AudioSource musicSource;
    public float bpm = 120f;

    private float secondsPerBeat;
    private double songStartDspTime;

    public static float SongPosition { get; private set; }

    void Start()
    {
        secondsPerBeat = 60f / bpm;
        songStartDspTime = AudioSettings.dspTime;
        musicSource.Play();
    }

    void Update()
    {
        SongPosition = (float)(AudioSettings.dspTime - songStartDspTime);
    }

    public float GetBeatTime() => SongPosition / secondsPerBeat;
}