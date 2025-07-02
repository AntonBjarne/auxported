using UnityEngine;

public class InputManager : MonoBehaviour
{
    public float hitWindow = 0.15f;

    public GameObject hitScorePrefab;  // Assign your hit score popup prefab here

    public Transform hitScorePosLeft;
    public Transform hitScorePosMiddle;
    public Transform hitScorePosRight;
    public Transform hitScorePosSpecial;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) HandleNoteHit(NoteType.Left);
        if (Input.GetKeyDown(KeyCode.S)) HandleNoteHit(NoteType.Middle);
        if (Input.GetKeyDown(KeyCode.D)) HandleNoteHit(NoteType.Right);
        if (Input.GetKeyDown(KeyCode.Space)) HandleNoteHit(NoteType.Special);
    }

    private void HandleNoteHit(NoteType type)
    {
        float closestNoteTime = NoteManager.Instance.GetClosestNoteTime(type);

        if (closestNoteTime < 0f)
        {
            ShowHitScore("Miss", type);
            Debug.Log($"No {type} note available");
            return;
        }

        float diff = Mathf.Abs(BeatManager.SongPosition - closestNoteTime);

        if (diff <= hitWindow)
        {
            string judgment = GetJudgment(diff);
            ShowHitScore(judgment, type);
            Debug.Log($"{judgment} hit on {type}");
            NoteManager.Instance.RemoveClosestNote(type);
        }
        else
        {
            ShowHitScore("Miss", type);
            Debug.Log($"Missed {type} note");
        }
    }

    private void ShowHitScore(string judgment, NoteType type)
    {
        Vector3 spawnPos = type switch
        {
            NoteType.Left => hitScorePosLeft != null ? hitScorePosLeft.position : Vector3.zero,
            NoteType.Middle => hitScorePosMiddle != null ? hitScorePosMiddle.position : Vector3.zero,
            NoteType.Right => hitScorePosRight != null ? hitScorePosRight.position : Vector3.zero,
            NoteType.Special => hitScorePosSpecial != null ? hitScorePosSpecial.position : Vector3.zero,
            _ => Vector3.zero
        };

        GameObject popup = Instantiate(hitScorePrefab, spawnPos, Quaternion.identity);

        HitScore hitScore = popup.GetComponent<HitScore>();
        if (hitScore != null)
        {
            hitScore.SetText(judgment);
        }
    }

    private string GetJudgment(float diff)
    {
        if (diff <= 0.05f) return "Perfect";
        if (diff <= 0.15f) return "Good";
        return "Miss";
    }
}
