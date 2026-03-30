using TMPro;
using Unity.Mathematics;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class AttackControl : MonoBehaviour
{
    [SerializeField]
    private CooldownUI cooldownUI;

    [SerializeField]
    private GameObject handSlotL;
    [SerializeField]
    private GameObject handSlotR;

    public int entityType; // 0 = hard, 1 = sunny, 2 = scrambled, 3 = poached, 4 = goons, 5 = devilled egg
    private SwordAttack swordAttackScript;

    public bool isAtacking;

    private void Awake()
    {
        if (GetComponent<SwordAttack>()) swordAttackScript = GetComponent<SwordAttack>();
        cooldownUI.GetComponent<CooldownUI>();
    }
    public void Attack(bool isMainAttack, int entityType)
    {
        // Right-handed (main) attack
        if (isMainAttack) 
        {
            switch (entityType)
            {
                // Hard boiled
                case 0:
                    swordAttackScript.attack(handSlotR);
                    // attacks too fast, no cooldown needed
                    //Debug.Log(handSlotR.GetComponentInChildren<WeaponData>().swingSpeed);
                    //cooldownUI.cooldownTimer(isMainAttack, handSlotR.GetComponentInChildren<WeaponData>().swingSpeed);
                    break;

                // Sunny side up
                case 1:
                    handSlotR.GetComponent<SunnyAttack>().Attack();
                    //Debug.Log(handSlotR.GetComponent<SunnyAttack>().projectile.GetComponent<WeaponData>().swingSpeed);
                    // If lost, the inputs are this: cooldownTimer(isMainAttack, duration)
                    cooldownUI.cooldownTimer(isMainAttack, handSlotR.GetComponent<SunnyAttack>().projectile.GetComponent<WeaponData>().swingSpeed);
                    break;

                // Scrambled
                case 2:
                    swordAttackScript.attack(handSlotR);
                    Debug.Log("scrambled egg attacks!");
                    break;

                // Poached
                case 3:
                    Debug.Log("poached egg attacks!");
                    break;

                // Goon
                case 4:
                    swordAttackScript.attack(handSlotR);
                    break;

                // Devilled egg
                case 5:
                    swordAttackScript.attack(handSlotR);
                    break;

                default:
                    break;
            }
        }

        // Left-handed (secondary) action
        else
        {
            switch (entityType)
            {
                // Hard boiled
                case 0:
                    Debug.Log("hard boiled egg has no secondary action");
                    break;

                // Sunny side up
                case 1:
                    // Throw salt
                    handSlotL.GetComponent<SunnyAttack>().Attack();
                    Debug.Log(handSlotL.GetComponent<SunnyAttack>().projectile.GetComponent<WeaponData>().swingSpeed);
                    cooldownUI.cooldownTimer(isMainAttack, handSlotL.GetComponent<SunnyAttack>().projectile.GetComponent<WeaponData>().swingSpeed);
                    break;

                // Scrambled
                case 2:
                    // Heal
                    handSlotL.GetComponent<scrambleHeal>().Heal();
                    Debug.Log("Recharge timer: " + handSlotL.GetComponent<scrambleHeal>().rechargeDuration);
                    cooldownUI.cooldownTimer(isMainAttack, handSlotL.GetComponent<scrambleHeal>().rechargeDuration);
                    break;

                // Poached
                case 3:
                    // Cast yolk trap
                    Debug.Log("poached egg has no secondary action");
                    break;

                // Goon
                case 4:
                    Debug.Log("goon has no secondary action");
                    break;

                // Devilled Egg
                case 5:
                    // Paprika storm
                    Debug.Log("devilled egg has no secondary action");
                    break;

                default:
                    break;
            }
        }
    }
}
