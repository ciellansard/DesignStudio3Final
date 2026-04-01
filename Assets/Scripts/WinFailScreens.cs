using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class WinFailScreens : MonoBehaviour {
    [SerializeField] private CharacterSelectorData imageData;
    [SerializeField] private Image playerImage;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject deviledEgg;

    private int playerHealth = 10;
    private int deviledEggHealth = 10;

    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject failPanel;
    [SerializeField] private GameObject hudCanvas;

    void Start() {
        deviledEgg = GameObject.Find("Devilled Egg");
        winPanel.SetActive(false);
        failPanel.SetActive(false);
    }

    private IEnumerator setHealth() {
        yield return null;
        //Debug.Log("DevilHealth: " + deviledEgg.GetComponent<CharacterHealth>().currentHealth);
        playerHealth = player.GetComponent<CharacterHealth>().currentHealth;
        deviledEggHealth = deviledEgg.GetComponent<CharacterHealth>().currentHealth;
    }

    void Update() {
        StartCoroutine(setHealth());
        Debug.Log("PlayerHealth: " + playerHealth);
        Debug.Log("DevilHealth: " + deviledEggHealth);
        if (playerHealth <= 0)
            ShowFailScreen();
        if (deviledEggHealth <= 0)
            StartCoroutine(ShowWinScreen());
    }

    private IEnumerator ShowWinScreen() {
        yield return new WaitForSeconds(3);
        winPanel.SetActive(true);
        hudCanvas.SetActive(false);
        playerImage.sprite = imageData.charPreviewImg[imageData.selectedIndex];
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void ShowFailScreen() {
        failPanel.SetActive(true);
        hudCanvas.SetActive(false);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void BackToMenu() {
        winPanel.SetActive(false);
        failPanel.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("startupMenu");
    }

}
