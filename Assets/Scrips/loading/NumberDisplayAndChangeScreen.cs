using UnityEngine;
using TMPro;
using System.Collections;

public class NumberDisplayAndChangePanel : MonoBehaviour
{
    public TMP_Text numberText;

    [SerializeField]
    private GameObject nextPanel;

    private void Start()
    {
        // Call a coroutine to display numbers from 0 to 100
        StartCoroutine(DisplayNumbers());
    }

    private IEnumerator DisplayNumbers()
    {
        for (int i = 0; i <= 100; i++)
        {
            // Update the text with the current number
            numberText.text = i.ToString();

            // Wait for a short period to see the numbers changing
            yield return new WaitForSeconds(0.005f);
        }

        // After completing the count, change the panel
        ChangePanel();
    }

    private void ChangePanel()
    {
        // Deactivate the current panel
        gameObject.SetActive(false);

        // Activate the next panel
        if (nextPanel != null)
        {
            nextPanel.SetActive(true);
        }
        else
        {
            Debug.LogError("Next panel not assigned.");
        }
    }
}
