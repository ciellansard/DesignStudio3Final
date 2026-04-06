using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

    private SoundManager soundManager;
    public bool goAgain = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
    }

    public void PlayGame() {
        soundManager.PlayMusic(soundManager.beautifulJazz);
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