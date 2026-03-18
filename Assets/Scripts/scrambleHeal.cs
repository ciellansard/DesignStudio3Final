using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class scrambleHeal : MonoBehaviour
{
    public int healPoints = 2;
    bool waitTime = true;
    Coroutine rechargeHeal;
    public GameObject player;

    public void Heal()
    {
        if (waitTime == true)
        {
            waitTime = false;

            if (player.GetComponent<CharacterHealth>().currentHealth < 20) {

                player.GetComponent<CharacterHealth>().currentHealth = player.GetComponent<CharacterHealth>().currentHealth + healPoints;

                if (player.GetComponent<CharacterHealth>().currentHealth >= 20)
                {
                    player.GetComponent<CharacterHealth>().currentHealth = 20;
                }
                rechargeHeal = StartCoroutine(RechargeWait(3));
            }
        }
        else
        {
            UnityEngine.Debug.Log("Healing not recharged");
        }

    }

    private void Update()
    {
        UnityEngine.Debug.Log(player.GetComponent<CharacterHealth>().currentHealth);
    }

    private IEnumerator RechargeWait(float duration)
    {
        yield return new WaitForSeconds(duration);
        waitTime = true;
    }
}
