using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

    [SerializeField] private string sceneToLoad;

    // custom data for buttons on the main menu
    public enum MenuButtons {
        play,
        settings,
        exit,
        credits
    };

    public void MenuButtonClicked(MenuButtons buttonClicked) {
        //Debug.Log("Button clicked: " + buttonClicked.ToString()); //testing purposes
        switch (buttonClicked) {
            case MenuButtons.play:
                SceneManager.LoadScene(sceneToLoad);
                break;
            case MenuButtons.settings:
                break;
            case MenuButtons.exit:
                Application.Quit();
                break;
            case MenuButtons.credits:
                break;
            default:
                Debug.Log("No buttonType for selected button.");
                break;
        }

    }

}