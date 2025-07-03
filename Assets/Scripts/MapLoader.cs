using System.IO;
using UnityEngine;

public class MapLoader : MonoBehaviour
{
    public string mapFileName = "map1.json"; // Name of the JSON file in StreamingAssets
    public AudioSource musicSource;          // 🎵 Drag your AudioSource here in the inspector

    void Start()
    {
        LoadMap(mapFileName);
    }

    public void LoadMap(string mapName)
    {
        // Path to JSON file
        string filePath = Path.Combine(Application.streamingAssetsPath, mapName);

        if (!File.Exists(filePath))
        {
            Debug.LogError($"Map file not found at {filePath}");
            return;
        }

        // Load JSON text
        string json = File.ReadAllText(filePath);
        MapData map = JsonUtility.FromJson<MapData>(json);

        Debug.Log($"Loaded map: {map.songName}, BPM: {map.bpm}, Notes: {map.notes.Count}");

        // Load audio file from Resources/Music/
        AudioClip clip = Resources.Load<AudioClip>("Music/" + Path.GetFileNameWithoutExtension(map.songFile));

        if (clip != null)
        {
            musicSource.clip = clip;
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning($"Could not find audio file: {map.songFile} in Resources/Music/");
        }

        // Set BPM and offset
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
