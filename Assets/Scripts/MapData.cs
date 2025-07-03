using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NoteEvent
{
    public NoteType type;
    public float time;
}

[Serializable]
public class MapData
{
    public string songName;     // Display name of the song
    public string songFile;     // Path or name of the audio file (e.g. "MySong.mp3")
    public float bpm;
    public float offset;
    public List<NoteEvent> notes;
}
