using UnityEngine;

[CreateAssetMenu(menuName = "Character Selector Data")]
public class CharacterSelectorData : ScriptableObject {
    // saving values and making them work across scenes
    public GameObject[] characters;
    public Sprite[] charPreviewImg;
    public int selectedIndex;
}
