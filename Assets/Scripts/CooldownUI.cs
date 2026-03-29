using UnityEngine;
using TMPro;
using System.Collections;

public class CooldownUI : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI abilityCoolLeft;
    [SerializeField] private TextMeshProUGUI abilityCoolRight;

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

    private IEnumerator RunCooldown(TextMeshProUGUI display, float duration, bool isMainAttack) {
        // set the correct check on cooldown start
        if (isMainAttack) rightOnCooldown = true;
        else leftOnCooldown = true;

        display.gameObject.SetActive(true);

        for (int i = (int)duration; i > 0; i--) {
            display.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        display.gameObject.SetActive(false);

        // clear the correct check when cooldown finishes
        if (isMainAttack) rightOnCooldown = false;
        else leftOnCooldown = false;
    }
}
