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
    public string songName;
    public float bpm;
    public float offset;
    public List<NoteEvent> notes;
}
