using UnityEngine;

[CreateAssetMenu(menuName = "Character Selector Data")]
public class CharacterSelectorData : ScriptableObject {
    public GameObject[] characters;
    public Sprite[] charPreviewImg;
    public int selectedIndex;
}
