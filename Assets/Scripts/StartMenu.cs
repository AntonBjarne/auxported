using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using Unity.VisualScripting;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    public GameObject startPanel;
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

        foreach (TextAsset mapFile in mapFiles)
        {
            string mapName = mapFile.name;

            GameObject buttonObj = Instantiate(songButtonPrefab, songListParent);

            buttonObj.GetComponentInChildren<Text>().text = mapName;

            buttonObj.GetComponent<Button>().onClick.AddListener(() => OnSongSelected(mapName));
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
