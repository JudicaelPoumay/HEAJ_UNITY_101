using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Resume normal time flow
        isPaused = false;
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Stop time flow
        isPaused = true;
    }

    public void RestartGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Ensure that time is flowing when restarting
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Restart current scene
    }

    public void QuitGame()
    {
        Time.timeScale = 1f; // Ensure that time is flowing when quitting
        Debug.Log("Quitting game..."); // Replace with actual quit functionality if needed
        Application.Quit();
    }
}