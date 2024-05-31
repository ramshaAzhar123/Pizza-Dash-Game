using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject currentPanel;
    [SerializeField] private GameObject nextPanel;

    private void Start()
    {
        // Ensure the current panel is active initially
        if (currentPanel != null)
            currentPanel.SetActive(true);
    }

    public void SwitchToNextPanel()
    {
        // Deactivate the current panel
        if (currentPanel != null)
            currentPanel.SetActive(false);

        // Activate the next panel
        if (nextPanel != null)
            nextPanel.SetActive(true);
        else
            Debug.LogError("Next panel is not assigned.");
    }

    public void SwitchToPreviousPanel()
    {
        // Deactivate the current panel
        if (currentPanel != null)
            currentPanel.SetActive(false);

        // Activate the previous panel
        if (nextPanel != null)
            nextPanel.SetActive(false);

        // Swap the panels
        GameObject temp = currentPanel;
        currentPanel = nextPanel;
        nextPanel = temp;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object has the "enemy" tag
        if (collision.gameObject.CompareTag("enemy"))
        {
            // Switch to the next panel
            SwitchToNextPanel();
        }
    }
}
