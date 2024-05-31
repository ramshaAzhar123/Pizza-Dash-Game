using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static int ScoreValue = 0;
    public Text scoreText;

    [SerializeField] private Button pauseButton;
    [SerializeField] private Button resumeButton;
    private bool isPaused = false;

    void Start()
    {
        UpdateUI();
        // Add listeners to the pause and resume buttons
        pauseButton.onClick.AddListener(PauseGame);
        resumeButton.onClick.AddListener(ResumeGame);
    }

    void Update()
    {
        if (!isPaused)
        {
            ScoreValue++; // Adjust this based on how you want the score to increment

            if (ScoreValue >= 16)
            {
                // Handle game over or winning conditions here
            }

            UpdateUI();
        }
    }

    void UpdateUI()
    {
        scoreText.text = "Score: " + ScoreValue;
    }

    public void ResetScore()
    {
        ScoreValue = 0;
        UpdateUI();
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f; // Pause the game
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f; // Resume the game
    }
}
