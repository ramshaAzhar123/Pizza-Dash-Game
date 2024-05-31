using UnityEngine;
using TMPro;

public class Texttimer : MonoBehaviour
{
    public TMP_Text displayText; // Use TMP_Text for TextMeshPro
    private float displayTime = 1.8f; // Adjust this to change the duration of text display
    private float timer = 0f;
    private bool hasDisplayed = false;

    void Update()
    {
        // Check if the text has been displayed and if the time limit hasn't exceeded
        if (!hasDisplayed && timer < displayTime)
        {
            // Display the text
            displayText.enabled = true;
            timer += Time.deltaTime;
        }
        else
        {
            // Text has been displayed for the specified duration, hide it
            displayText.enabled = false;
            hasDisplayed = true;
        }
    }
}
