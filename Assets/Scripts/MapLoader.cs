using UnityEngine;

public class MapLoader : MonoBehaviour
{
    public BeatManager beatManager;  // Assign this in Inspector!

    public AudioClip songClip;
    public float bpm;
    public float offset;

    void Start()
    {
        if (beatManager == null)
        {
            Debug.LogError("BeatManager reference is missing in MapLoader!");
            return;
        }

        beatManager.SetBpm(bpm);

        // Assuming you want to assign the song clip to the BeatManager's AudioSource:
        if (beatManager.musicSource != null)
        {
            beatManager.musicSource.clip = songClip;
            beatManager.musicSource.PlayDelayed(offset);
        }
        else
        {
            Debug.LogError("BeatManager's AudioSource is missing!");
        }
    }
}
