using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    [SerializeField] private CooldownUI cooldownUI;

    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private GameObject hudCanvas;

    private bool isPaused = false;

    void Update() {
        if (Keyboard.current.escapeKey.wasPressedThisFrame) {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame() {
        cooldownUI.OnPause();
        pauseMenuPanel.SetActive(true);
        hudCanvas.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame() {
        cooldownUI.OnResume();
        pauseMenuPanel.SetActive(false);
        hudCanvas.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void QuitToMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("startupMenu");
    }

    public void QuitToDesktop() {
        Application.Quit();
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}