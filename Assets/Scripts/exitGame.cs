using UnityEngine;

//refrenced https://discussions.unity.com/t/how-do-i-create-a-exit-quit-button/142125/3
public class exitGame : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }
}
