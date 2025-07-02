using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public static NoteSpawner Instance;

    public GameObject noteLPrefab;
    public GameObject noteMPrefab;
    public GameObject noteRPrefab;
    public GameObject noteSPrefab;

    public Vector3 spawnPosition = new Vector3(0, 0, 7);


    public Transform spawnPoint;

    // Store timings along with note types for spawning
    public List<(NoteType type, float time)> notesToSpawn = new();

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        float songTime = BeatManager.SongPosition;

        for (int i = 0; i < notesToSpawn.Count; i++)
        {
            if (notesToSpawn[i].time <= songTime + 2f) // spawn 2s early
            {
                GameObject prefab = GetPrefabForNoteType(notesToSpawn[i].type);
                GameObject note = Instantiate(prefab, spawnPosition, Quaternion.identity);

                Note noteComp = note.GetComponent<Note>();
                noteComp.noteType = notesToSpawn[i].type;
                noteComp.targetTime = notesToSpawn[i].time;

                NoteManager.Instance.AddNote(notesToSpawn[i].type, note);

                notesToSpawn.RemoveAt(i);
                i--;
            }
        }
    }


    GameObject GetPrefabForNoteType(NoteType type)
    {
        return type switch
        {
            NoteType.Left => noteLPrefab,
            NoteType.Middle => noteMPrefab,
            NoteType.Right => noteRPrefab,
            NoteType.Special => noteSPrefab,
            _ => null,
        };
    }
}
