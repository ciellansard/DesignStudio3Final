using TMPro;
using Unity.Mathematics;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

// TO-DO: maybe create script for every attack type (like how SunnyAttack has its own script)


public class AttackControl : MonoBehaviour
{
    [SerializeField]
    private GameObject handSlotL;
    [SerializeField]
    private GameObject handSlotR;
    public Quaternion targetRotation = Quaternion.Euler(90,0,0);
    private Quaternion restPosition;
    public Coroutine rotateCoroutine;
    bool swingingDown = true;

    public int entityType; // 0 = hard, 1 = sunny, 2 = scrambled, 3 = poached, 4 = goons, 5 = devilled egg


    public void Awake()
    {
        restPosition = handSlotR.transform.rotation;
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

                    if (rotateCoroutine != null) return;

                    swingingDown = true;

                    //this is a disgusting call, but i don't have a better solution at the moment
                    rotateCoroutine = StartCoroutine(RotateToAngle(handSlotR.transform.GetComponentInChildren<WeaponData>().swingSpeed));
                    Debug.Log("hard boiled egg attacks!");

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
                    Debug.Log("poached egg attacks!");
                    break;

                // Goon
                case 4:
                    if (rotateCoroutine != null) return;

                    swingingDown = true;

                    //this is a disgusting call, but i don't have a better solution at the moment
                    rotateCoroutine = StartCoroutine(RotateToAngle(handSlotR.transform.GetComponentInChildren<WeaponData>().swingSpeed));
                    Debug.Log("goon attacks!");

                    break;

                // Devilled egg
                case 5:
                    if (rotateCoroutine != null) return;

                    swingingDown = true;

                    //this is a disgusting call, but i don't have a better solution at the moment
                    rotateCoroutine = StartCoroutine(RotateToAngle(handSlotR.transform.GetComponentInChildren<WeaponData>().swingSpeed));
                    Debug.Log("devilled egg attacks!");

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





    private IEnumerator RotateToAngle(float swingSpeed)
    {
        //rotate down
        while (Quaternion.Angle(handSlotR.transform.localRotation, targetRotation) > 0.1f && swingingDown)
        {
            Debug.Log("swinging down");
            handSlotR.transform.localRotation = Quaternion.RotateTowards(handSlotR.transform.localRotation, targetRotation, swingSpeed * Time.deltaTime);
            if (Quaternion.Angle(handSlotR.transform.localRotation, targetRotation) < 0.1f) swingingDown = false;
            yield return null;
        }
        //rotate back up to rest
        while (Quaternion.Angle(handSlotR.transform.localRotation, restPosition) > 0.1f)
        {
            Debug.Log("swinging up");
            handSlotR.transform.localRotation = Quaternion.RotateTowards(handSlotR.transform.localRotation, restPosition, swingSpeed * Time.deltaTime);
            yield return null;
        }

        handSlotR.transform.localRotation = restPosition;
        rotateCoroutine = null;
    }
}
