using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject canvas; // Reference to your Canvas GameObject

    public void OnYesButtonClicked()
    {
        GameManager.Instance.EndGame(); // Call the EndGame method in GameManager
    }

    public void OnNoButtonClicked()
    {
        // Implement any other action you want when the player clicks No, such as quitting the game
        Application.Quit();
    }

    // Method to show the canvas
    public void ShowCanvas()
    {
        if (canvas != null)
        {
            canvas.SetActive(true); // Activate the canvas
        }
        else
        {
            Debug.LogError("Canvas GameObject reference is not set in UIManager.");
        }
    }

    // Method to hide the canvas
    public void HideCanvas()
    {
        if (canvas != null)
        {
            canvas.SetActive(false); // Deactivate the canvas
        }
        else
        {
            Debug.LogError("Canvas GameObject reference is not set in UIManager.");
        }
    }
}