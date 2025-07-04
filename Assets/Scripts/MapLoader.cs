using UnityEngine;
using static SongListManager;

public class MapLoader : MonoBehaviour
{
    public AudioSource musicSource; // Assign in inspector

    void Start()
    {
        MapData map = SelectedMapDataHolder.SelectedMap;

        if (map == null)
        {
            Debug.LogError("No map selected! Did you come from the start scene?");
            return;
        }

        LoadMap(map);
    }

    public void LoadMap(MapData map)
    {
        Debug.Log($"Loaded map: {map.songName}, BPM: {map.bpm}, Notes: {map.notes.Count}");

        // Load audio file from Resources/Music/ folder
        AudioClip clip = Resources.Load<AudioClip>("Music/" + System.IO.Path.GetFileNameWithoutExtension(map.songFile));

        if (clip != null)
        {
            musicSource.clip = clip;
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning($"Could not find audio file: {map.songFile} in Resources/Music/");
        }

        // Set BPM and offset (make sure BeatManager is ready and has these methods)
        BeatManager.Instance.SetBpm(map.bpm);
        BeatManager.Instance.SetOffset(map.offset);

        // Pass notes to NoteSpawner
        NoteSpawner.Instance.notesToSpawn.Clear();
        foreach (var note in map.notes)
        {
            NoteSpawner.Instance.notesToSpawn.Add((note.type, note.time));
        }
    }
}
