using UnityEngine;
using TMPro; // Import TextMeshPro namespace

public class SauceScore : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI totalScoreText; // Use TextMeshProUGUI instead of Text

    void Start()
    {
        // Retrieve the saved final score from PlayerPrefs
        int finalScore = PlayerPrefs.GetInt(": ");

        // Debug log to check if the value is retrieved correctly
        Debug.Log(": " + finalScore);

        // Display the final score
        totalScoreText.text = ": " + finalScore.ToString();
    }
}
