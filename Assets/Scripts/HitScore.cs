using UnityEngine;
using UnityEngine.UI;  // If you are using UI Text

public class HitScore : MonoBehaviour
{
    public Text scoreText;  // Assign in inspector (or use TMPro.TextMeshProUGUI if you use TextMeshPro)

    public void SetText(string text)
    {
        if (scoreText != null)
        {
            scoreText.text = text;
        }
        else
        {
            Debug.LogWarning("ScoreText is not assigned on HitScore!");
        }
    }
}
