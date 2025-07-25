using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using Unity.VisualScripting;
using UnityEngine.UI;
using TMPro;

public class StartMenu : MonoBehaviour
{
    public GameObject startPanel; //JAG VAR INLOGGAD PÅ FEL SKIT MEN NU KANSKE DET GÅR ATT PUSHA IGENERGNADBJADFGADFGBJNIADFGBJIGABJIGABJIKAFGBJIKADFG
    public GameObject songSelectionPanel; // fäst dessa i inspector
    public Transform songListParent;
    public GameObject songButtonPrefab;

    void Start() //Start meny aktiv songselect inaktiv
    {
        startPanel.SetActive(true);
        songSelectionPanel.SetActive(false);

    }

    public void OnStartButton() //byt panel
    {
        startPanel.SetActive(false);
        songSelectionPanel.SetActive(true);
        PopulateSongList();
    }

    public void OnQuitButton() //avsluta
    { 
        Application.Quit();
        Debug.Log("Quit Game");
    }

    public void OnBackButton() //gå från songselect tilbaka till start
    {
        songSelectionPanel.SetActive(false);
        startPanel.SetActive(true);

        foreach (Transform child in songListParent)
        {
            Destroy(child.gameObject);
        }
    }

    void PopulateSongList()
    {
        TextAsset[] mapFiles = Resources.LoadAll<TextAsset>("Maps");
        Debug.Log($"Found {mapFiles.Length} map files");

        foreach (TextAsset mapFile in mapFiles)
        {
            MapData mapData = JsonUtility.FromJson<MapData>(mapFile.text);
            Debug.Log($"Loaded song name: {mapData.SongName}");

            GameObject buttonObj = Instantiate(songButtonPrefab, songListParent);

            // Use TextMeshProUGUI instead of Text
            var tmpText = buttonObj.GetComponentInChildren<TextMeshProUGUI>();
            if (tmpText != null)
                tmpText.text = mapData.SongName;
            else
                Debug.LogWarning("No TextMeshProUGUI component found in button prefab!");

            buttonObj.GetComponent<Button>().onClick.AddListener(() => OnSongSelected(mapFile.name));
        }
    }

    void OnSongSelected(string mapName)
    {
        Debug.Log("Selected map: " + mapName);

        // Later: load map in MapLoader
    }

    /*public void LoadGameScene(string mapName)
    {
        SelectedMapHolder.SelectedMapName = mapName;
        SceneManager.LoadScene("GameScene");
    }*/
}
