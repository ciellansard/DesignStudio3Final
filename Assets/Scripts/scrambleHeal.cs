using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class scrambleHeal : MonoBehaviour
{
    public int healPoints = 2;
    public int rechargeDuration = 2;
    bool waitTime = true;
    Coroutine rechargeHeal;
    public GameObject player;
    public ParticleSystem healEffect;
    int defaultHealth;

    [SerializeField]
    private EggBarIcons hudScript;

    private void Awake()
    {
        defaultHealth = player.GetComponent<CharacterHealth>().maxHealth;
        healEffect.Stop();
    }

    public void Heal()
    {
        if (waitTime == true)
        {
            waitTime = false;

            if (player.GetComponent<CharacterHealth>().currentHealth < defaultHealth)
            {
                player.GetComponent<CharacterHealth>().currentHealth = player.GetComponent<CharacterHealth>().currentHealth + healPoints;

                healEffect.Play();

                if (player.GetComponent<CharacterHealth>().currentHealth >= defaultHealth)
                {
                    player.GetComponent<CharacterHealth>().currentHealth = defaultHealth;
                    UnityEngine.Debug.Log("Health full");
                }
                rechargeHeal = StartCoroutine(RechargeWait(rechargeDuration));
            }
        }
        else
        {
            UnityEngine.Debug.Log("Healing not recharged");
        }

        hudScript.UpdateHealth(player.GetComponent<CharacterHealth>().currentHealth);
        UnityEngine.Debug.Log(player.GetComponent<CharacterHealth>().currentHealth);
    }

    private IEnumerator RechargeWait(float duration)
    {
        yield return new WaitForSeconds(duration);
        waitTime = true;
    }
}
