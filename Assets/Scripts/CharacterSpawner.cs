using UnityEngine;

public class CharacterSpawner : MonoBehaviour {
    public CharacterSelectorData selectorData;

    void Start() {
        Instantiate(selectorData.characters[selectorData.selectedIndex], transform.position, transform.rotation);
    }
}
