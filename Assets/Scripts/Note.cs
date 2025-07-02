using UnityEngine;

public class Note : MonoBehaviour
{
    public float targetTime;
    public Transform hitPoint;
    public Transform spawnPoint;
    public float travelTime = 2f;

    public NoteType noteType;

    void Update()
    {
        float timeLeft = targetTime - BeatManager.SongPosition;
        float t = 1f - (timeLeft / travelTime);
        transform.position = Vector3.Lerp(spawnPoint.position, hitPoint.position, t);

        if (timeLeft < -0.3f)
        {
            Debug.Log("Missed note");
            Destroy(gameObject);
        }
    }
}
