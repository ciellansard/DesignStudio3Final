using UnityEngine;
using UnityEngine.UI;

public class CharacterSelector : MonoBehaviour {
    // a link to the selector data
    public CharacterSelectorData selectorData;

    // UI elements
    public Image charPreviewImg;
    public Button leftButton;
    public Button rightButton;

    void Start() {
        leftButton.onClick.AddListener(CycleLeft);
        rightButton.onClick.AddListener(CycleRight);
        UpdateDisplay();
    }

    void CycleLeft() {
        selectorData.selectedIndex--;
        if (selectorData.selectedIndex < 0)
            selectorData.selectedIndex = selectorData.characters.Length - 1;
        UpdateDisplay();
    }

    void CycleRight() {
        selectorData.selectedIndex++;
        if (selectorData.selectedIndex >= selectorData.characters.Length)
            selectorData.selectedIndex = 0;
        UpdateDisplay();
    }

    void UpdateDisplay() {
        charPreviewImg.sprite = selectorData.charPreviewImg[selectorData.selectedIndex];
    }
}
