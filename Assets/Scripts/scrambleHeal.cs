using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class scrambleHeal : MonoBehaviour
{
    public int healPoints = 2;
    public int rechargeDuration = 3;
    bool waitTime = true;
    Coroutine rechargeHeal;
    public GameObject player;
    int defaultHealth;

    private void Awake()
    {
        player.GetComponent<CharacterHealth>().maxHealth = defaultHealth;
    }

    public void Heal()
    {
        if (waitTime == true)
        {
            waitTime = false;

            if (player.GetComponent<CharacterHealth>().currentHealth < defaultHealth) {

                player.GetComponent<CharacterHealth>().currentHealth = player.GetComponent<CharacterHealth>().currentHealth + healPoints;

                if (player.GetComponent<CharacterHealth>().currentHealth >= defaultHealth)
                {
                    player.GetComponent<CharacterHealth>().currentHealth = defaultHealth;
                }
                rechargeHeal = StartCoroutine(RechargeWait(rechargeDuration));
            }
        }
        else
        {
            UnityEngine.Debug.Log("Healing not recharged");
        }

    }

    private IEnumerator RechargeWait(float duration)
    {
        yield return new WaitForSeconds(duration);
        waitTime = true;
    }
}
