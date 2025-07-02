using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public static NoteManager Instance;

    private Dictionary<NoteType, List<GameObject>> activeNotes = new();

    void Awake()
    {
        Instance = this;

        foreach (NoteType type in System.Enum.GetValues(typeof(NoteType)))
        {
            activeNotes[type] = new List<GameObject>();
        }
    }

    public void AddNote(NoteType type, GameObject note)
    {
        activeNotes[type].Add(note);
    }

    public float GetClosestNoteTime(NoteType type)
    {
        if (activeNotes[type].Count == 0) return -1f;

        GameObject closest = null;
        float smallestDiff = float.MaxValue;

        foreach (var note in activeNotes[type])
        {
            float diff = Mathf.Abs(note.GetComponent<Note>().targetTime - BeatManager.SongPosition);
            if (diff < smallestDiff)
            {
                smallestDiff = diff;
                closest = note;
            }
        }

        return closest != null ? closest.GetComponent<Note>().targetTime : -1f;
    }

    public void RemoveClosestNote(NoteType type)
    {
        if (activeNotes[type].Count == 0) return;

        GameObject closest = null;
        float smallestDiff = float.MaxValue;

        foreach (var note in activeNotes[type])
        {
            float diff = Mathf.Abs(note.GetComponent<Note>().targetTime - BeatManager.SongPosition);
            if (diff < smallestDiff)
            {
                smallestDiff = diff;
                closest = note;
            }
        }

        if (closest != null)
        {
            activeNotes[type].Remove(closest);
            Destroy(closest);
        }
    }
}
