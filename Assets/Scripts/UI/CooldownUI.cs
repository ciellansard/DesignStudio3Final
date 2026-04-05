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

    private Coroutine leftCooldown;
    private Coroutine rightCooldown;

    private float rightTimeRemaining = 0f;
    private float leftTimeRemaining = 0f;

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
        float timeRemaining = duration;
        while (timeRemaining > 0) {
            // save remaining time each frame to resume after pause
            if (isMainAttack) rightTimeRemaining = timeRemaining;
            else leftTimeRemaining = timeRemaining;

            display.text = Mathf.CeilToInt(timeRemaining).ToString();
            timeRemaining -= Time.deltaTime;
            // wait one frame at a time
            yield return null; 
        }

        // hide
        display.gameObject.SetActive(false);

        // clear the correct check when cooldown finishes
        if (isMainAttack) rightOnCooldown = false;
        else leftOnCooldown = false;
    }

    public void OnPause() {
        // stop coroutines
        if (rightCooldown != null) StopCoroutine(rightCooldown);
        if (leftCooldown != null) StopCoroutine(leftCooldown);
    }

    public void OnResume() {
        // restart coroutines from saved remaining time
        if (rightOnCooldown)
            rightCooldown = StartCoroutine(RunCooldown(abilityCoolRight, rightTimeRemaining, true));
        if (leftOnCooldown)
            leftCooldown = StartCoroutine(RunCooldown(abilityCoolLeft, leftTimeRemaining, false));
    }
}
