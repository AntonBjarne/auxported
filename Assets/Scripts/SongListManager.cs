using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SongListManager : MonoBehaviour
{
    public GameObject songButtonPrefab;  // Assign a Button prefab in inspector
    public Transform contentParent;      // Assign the parent transform (e.g. SongList GameObject)

    private List<MapData> maps = new List<MapData>();

    void Start()
    {
        LoadAllMaps();
        PopulateSongButtons();
    }

    void LoadAllMaps()
    {
        // Assuming your JSON files are in Resources/Maps folder
        TextAsset[] mapJsonFiles = Resources.LoadAll<TextAsset>("Maps");
        foreach (var json in mapJsonFiles)
        {
            MapData map = JsonUtility.FromJson<MapData>(json.text);
            maps.Add(map);
        }
    }

    void PopulateSongButtons()
    {
        foreach (var map in maps)
        {
            GameObject btnObj = Instantiate(songButtonPrefab, contentParent);
            Button btn = btnObj.GetComponent<Button>();
            Text btnText = btnObj.GetComponentInChildren<Text>();
            btnText.text = map.songName;

            // Capture local copy of map for the listener
            MapData selectedMap = map;
            btn.onClick.AddListener(() => OnSongSelected(selectedMap));
        }
    }

    void OnSongSelected(MapData selectedMap)
    {
        // Save selected map info for the gameplay scene, e.g. using a static class or PlayerPrefs
        SelectedMapDataHolder.SelectedMap = selectedMap;

        // Load gameplay scene
        SceneManager.LoadScene("GameplayScene");  // change to your gameplay scene name
    }

    public static class SelectedMapDataHolder
    {
        public static MapData SelectedMap;
    }
}
