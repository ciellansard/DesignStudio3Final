using UnityEngine;

[CreateAssetMenu(menuName = "Character Selector Data")]
public class CharacterSelectorData : ScriptableObject {
    // saving values and making them work across scenes
    public GameObject[] characters;
    public GameObject[] VRCharacters;
    public Sprite[] charPreviewImg;
    public int selectedIndex;
    public bool VR;
    public bool VRSim;
    public bool SnapTurn;
    public bool Teleport;
}
