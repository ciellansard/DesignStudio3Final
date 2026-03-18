using UnityEngine;

public class MenuButtonManager : MonoBehaviour {
    
    public MainMenuManager menuManager;

    [SerializeField] private MainMenuManager.MenuButtons buttonType;

    public void ButtonClicked() {
        menuManager.MenuButtonClicked(buttonType);
    }
}
