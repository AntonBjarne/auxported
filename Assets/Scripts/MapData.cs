using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class MapData
{
    public string SongName;
    public string SongFile;
    public string Artist;
    public int Bpm;
    public int Offset;
    public List<NoteData> Notes; //lista över noter för senare

}

