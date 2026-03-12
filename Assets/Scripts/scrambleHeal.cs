using UnityEngine;
using System.Collections;

public class scrambleHeal : MonoBehaviour
{
    public int healPoints = 5;
    bool waitTime = true;
    Coroutine rechargeHeal;

    void Heal()
    {
        if(waitTime == true)
        {
            waitTime = false;
            gameObject.GetComponent<CharacterHealth>().currentHealth += healPoints;
            rechargeHeal = StartCoroutine(RechargeWait(5));
        }
    }

    private IEnumerator RechargeWait(float duration)
    {
        yield return new WaitForSeconds(duration);
        waitTime = false;
    }
}
