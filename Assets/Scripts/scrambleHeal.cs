using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class scrambleHeal : MonoBehaviour
{
    public int healPoints;
    public int rechargeDuration;
    bool waitTime = true;
    Coroutine rechargeHeal;
    public GameObject player;
    public ParticleSystem healEffect;
    int defaultHealth;

    [SerializeField]
    private EggBarIcons hudScript;
    private SoundManager soundManager;

    private void Awake()
    {
        healEffect.Stop();
        defaultHealth = player.GetComponent<CharacterHealth>().maxHealth;
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        //soundManager.PlaySound(healingSound);
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
                soundManager.PlaySound(soundManager.healing);
                soundManager.FadeOutSfx(1);

                if (player.GetComponent<CharacterHealth>().currentHealth >= defaultHealth)
                {
                    player.GetComponent<CharacterHealth>().currentHealth = defaultHealth;
                    UnityEngine.Debug.Log("Health full");
                    soundManager.PlaySound(soundManager.cheers);
                }

                //UnityEngine.Debug.Log("hi");
                rechargeHeal = StartCoroutine(RechargeWait(rechargeDuration));
            }
            else { waitTime = true;}
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
        //UnityEngine.Debug.Log("two");
        yield return new WaitForSeconds(duration);
        //UnityEngine.Debug.Log("three");
        waitTime = true;
        //UnityEngine.Debug.Log("four");
    }
}
