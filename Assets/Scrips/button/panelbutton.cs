using UnityEngine;

public class PanelSwitcher : MonoBehaviour
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
}
