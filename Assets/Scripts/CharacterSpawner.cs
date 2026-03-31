using UnityEngine;

public class CharacterSpawner : MonoBehaviour {
    public CharacterSelectorData selectorData;

    // Made a spawner that the player will spawn at.
    void Start() {
        if (selectorData.VR == false)
        {
            Instantiate(selectorData.characters[selectorData.selectedIndex], transform.position, transform.rotation);
        }
        else if (selectorData.VR == true)
        {
            GameObject player = Instantiate(selectorData.VRCharacters[selectorData.selectedIndex], transform.position, transform.rotation);
            GameObject.FindWithTag("Simulator").active = selectorData.VRSim;

            if (selectorData.SnapTurn == true)
            {
                GameObject.FindWithTag("Snap Turn").active = true;
                GameObject.FindWithTag("Smooth Turn").active = false;
            }
            else
            {
                GameObject.FindWithTag("Snap Turn").active = false;
                GameObject.FindWithTag("Smooth Turn").active = true;
            }

            if (selectorData.Teleport == true)
            {
                GameObject.FindWithTag("Teleport").active = true;
                GameObject.FindWithTag("Smooth Walk").active = false;
            }
            else
            {
                GameObject.FindWithTag("Teleport").active = false;
                GameObject.FindWithTag("Smooth Walk").active = true;
            }
        }
        
    }
}
