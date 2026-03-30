using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {
    public void PlayGame() {
        SceneManager.LoadScene("gameSet");
    }

    public void GameSettings() {
        // waiting for help
    }

    public void ExitGame() {
        Application.Quit();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}