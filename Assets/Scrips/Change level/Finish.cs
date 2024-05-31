using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    [SerializeField] private GameObject oldPanel;
    [SerializeField] private GameObject newPanel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            UnlockNewLevel();
            ChangePanel();
        }
    }

    void ChangePanel()
    {
        // Deactivate the old panel and activate the new panel
        oldPanel.SetActive(false);
        newPanel.SetActive(true);

        // You can still update PlayerPrefs to keep track of progress if needed
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneLoad = currentSceneIndex + 1;

        // Update the PlayerPrefs to unlock the next level
        if (nextSceneLoad > PlayerPrefs.GetInt("levelAt"))
        {
            PlayerPrefs.SetInt("levelAt", nextSceneLoad);
        }
    }

    void UnlockNewLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();
        }
    }
}
