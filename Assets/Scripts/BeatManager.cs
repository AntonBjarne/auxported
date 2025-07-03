using UnityEngine;

public class BeatManager : MonoBehaviour
{
    public static BeatManager Instance; // 👈 Singleton instance

    private float bpm = 120f;
    private float offset = 0f;

    private float startTime;

    public static float SongPosition { get; private set; }

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: Keep between scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        startTime = Time.time - offset;
    }

    void Update()
    {
        SongPosition = Time.time - startTime;
    }

    public void SetBpm(float bpmValue)
    {
        bpm = bpmValue;
    }

    public void SetOffset(float offsetValue)
    {
        offset = offsetValue;
    }

    public float GetBpm()
    {
        return bpm;
    }

    public float GetOffset()
    {
        return offset;
    }
}
