using UnityEngine;
using TMPro;
using System.Collections;

public class CooldownUI : MonoBehaviour {
    // the text that will be the cooldown timer
    [SerializeField] private TextMeshProUGUI abilityCoolLeft;
    [SerializeField] private TextMeshProUGUI abilityCoolRight;

    // a check to stop the cooldown from being run multiple times
    private bool rightOnCooldown = false;
    private bool leftOnCooldown = false;

    void Start() {
        abilityCoolLeft.gameObject.SetActive(false); 
        abilityCoolRight.gameObject.SetActive(false); 
    }


    public void cooldownTimer(bool isMainAttack, float duration){
        if (isMainAttack) {
            if (rightOnCooldown) return; 
            StartCoroutine(RunCooldown(abilityCoolRight, duration, isMainAttack));
        }
        else {
            if (leftOnCooldown) return; 
            StartCoroutine(RunCooldown(abilityCoolLeft, duration, isMainAttack));
        }
    }

    // coroutine to allow for control of timing for the cooldown timer
    // https://docs.unity3d.com/6000.3/Documentation/ScriptReference/WaitForSeconds.html
    private IEnumerator RunCooldown(TextMeshProUGUI display, float duration, bool isMainAttack) {
        // set the correct check on cooldown start
        if (isMainAttack) rightOnCooldown = true;
        else leftOnCooldown = true;

        // make the number show up
        display.gameObject.SetActive(true);

        // count down
        for (int i = (int)duration; i > 0; i--) {
            display.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        // hicde
        display.gameObject.SetActive(false);

        // clear the correct check when cooldown finishes
        if (isMainAttack) rightOnCooldown = false;
        else leftOnCooldown = false;
    }
}
