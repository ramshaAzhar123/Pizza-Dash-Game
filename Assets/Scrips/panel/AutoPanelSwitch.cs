using UnityEngine;
using System.Collections;

public class AutoPanelSwitch : MonoBehaviour
{
    [SerializeField] private GameObject currentPanel;
    [SerializeField] private GameObject nextPanel;
    [SerializeField] private float switchDelay = 15f; // Delay before switching to the next panel

    private void Start()
    {
        // Ensure the current panel is active initially
        if (currentPanel != null)
            currentPanel.SetActive(true);

        // Start the coroutine to switch panels automatically
        StartCoroutine(AutoSwitchCoroutine());
    }

    private IEnumerator AutoSwitchCoroutine()
    {
        // Wait for the specified delay before switching to the next panel
        yield return new WaitForSeconds(switchDelay);

        // Switch to the next panel
        SwitchToNextPanel();

        // Restart the coroutine to continue automatic switching
        StartCoroutine(AutoSwitchCoroutine());
    }

    private void SwitchToNextPanel()
    {
        // Deactivate the current panel
        if (currentPanel != null)
            currentPanel.SetActive(false);

        // Check if nextPanel is assigned before activating it
        if (nextPanel != null)
        {
            // Activate the next panel
            nextPanel.SetActive(true);

            // Swap the panels for the next automatic switch
            GameObject temp = currentPanel;
            currentPanel = nextPanel;
            nextPanel = temp;
        }
        else
        {
            Debug.LogError("Next panel is not assigned.");
        }
    }
}
