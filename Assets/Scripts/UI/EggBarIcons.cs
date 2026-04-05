using UnityEngine;
using UnityEngine.UI;

public class EggBarIcons : MonoBehaviour {
    // array of each egg sprite
    public Sprite[] eggState; // 0 = empty, 1 = cracked, 2 = full

    // A parent transform to keep the eggs as a bar
    public Transform eggBar;

    // link the egg prefab I made to this
    public GameObject eggPrefab;

    // Just a default for however much health the guy has. 
    // Each egg counts for 2 hp so multiply by 2
    // e.g. If Hard Boiled has 4 eggs, set this to 8
    // (idk how yolked each guy is so feel free to change)
    public int maxHP = 8;

    // Cached Image components for each egg slot
    private Image[] eggImages;

    private void Awake() {
        // auto-initialize if maxHP is already set in the inspector
        if (maxHP > 0)
            InitializeEggs(maxHP);
    }

    // Call this function in the script that controls player health.
    // Remember: each egg is worth 2 hit points. Double the number of however many eggs the dude should have 
    public void SetMaxHP(int newMaxHP) {
        if (newMaxHP < 1) {
            maxHP = 2; // making it so the guys can't start with less than one full egg.
            // You guys can remove if you want and just make it set maxHP to newMaxHP.
        }
        else {
            maxHP = newMaxHP;
        }

        InitializeEggs(maxHP);
    }

    // Call this function when the guy takes damega
    public void UpdateHealth(int currentHP) {
        if (eggImages == null) {
            Debug.LogWarning("Warning from EggBarIcons Script: Eggs not initialized. Call SetMaxHP() first.");
            return;
        }

        // Clamp HP to valid range just incase something crazy happens
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);

        for (int i = 0; i < eggImages.Length; i++) {
            // HP contributed by eggs before this one
            int hpBeforeThisEgg = i * 2;

            // How much HP "fills" this egg (0, 1, or 2)
            int hpInThisEgg = Mathf.Clamp(currentHP - hpBeforeThisEgg, 0, 2);

            eggImages[i].sprite = eggState[hpInThisEgg];
        }
    }




    private void InitializeEggs(int hp) {
        // Clear existing eggs
        foreach (Transform child in eggBar)
            Destroy(child.gameObject);

        int eggCount = Mathf.CeilToInt(hp / 2f);
        eggImages = new Image[eggCount];

        for (int i = 0; i < eggCount; i++) {
            GameObject eggGO = Instantiate(eggPrefab, eggBar);
            eggImages[i] = eggGO.GetComponent<Image>();

            if (eggImages[i] == null)
                Debug.LogError("Warning from EggBarIcons Script: eggPrefab is missing an Image component.");
        }

        // Default display: full health
        UpdateHealth(hp);
    }
}
