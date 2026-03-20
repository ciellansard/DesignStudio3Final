using TMPro;
using Unity.Mathematics;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class AttackControl : MonoBehaviour
{
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
                    break;

                // Sunny side up
                case 1:
                    handSlotR.GetComponent<SunnyAttack>().Attack();
                    break;

                // Scrambled
                case 2:
                    Debug.Log("scrambled egg attacks!");
                    break;

                // Poached
                case 3:
                    handSlotR.GetComponent<PoachedAttack>().Attack();
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
                    break;

                // Scrambled
                case 2:
                    // Heal
                    Debug.Log("scrambled egg has no secondary action");
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
