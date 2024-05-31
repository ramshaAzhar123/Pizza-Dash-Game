using UnityEngine;
using UnityEngine.SceneManagement;

public class button : MonoBehaviour
{
    // Add this script to your button GameObject

    // Specify the name of the scene you want to open
    public string sceneToOpen;

    // This method will be called when the button is clicked
    public void OnButtonClick()
    {
        // Load the specified scene
        SceneManager.LoadScene(sceneToOpen);
    }
}