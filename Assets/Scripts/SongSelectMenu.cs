using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class SongSelectMenu : MonoBehaviour
{
    public GameObject buttonPrefab;      // Assign your button prefab in inspector
    public Transform songListContainer;  // Assign the Vertical Layout Group here

    void Start()
    {
        LoadSongList();
    }

    void LoadSongList()
    {
        // Load all JSON map files in Resources/Maps
        TextAsset[] mapFiles = Resources.LoadAll<TextAsset>("Maps");

        foreach (var mapFile in mapFiles)
        {
            // Create a new button for each song
            GameObject newButton = Instantiate(buttonPrefab, songListContainer);
            newButton.GetComponentInChildren<Text>().text = mapFile.name;

            // Add click listener
            newButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                SelectSong(mapFile.name);
            });
        }
    }

    void SelectSong(string mapName)
    {
        Debug.Log("Selected map: " + mapName);
        PlayerPrefs.SetString("SelectedMap", mapName); // Save selected map name
        SceneManager.LoadScene("GameScene");           // Load your actual game scene
    }
}
