using UnityEngine;

public class CharacterSpawner : MonoBehaviour {
    public CharacterSelectorData selectorData;

    // Made a spawner that the player will spawn at.
    void Start() {
        Instantiate(selectorData.characters[selectorData.selectedIndex], transform.position, transform.rotation);
    }
}
