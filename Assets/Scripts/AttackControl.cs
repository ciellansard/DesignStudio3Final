using TMPro;
using Unity.Mathematics;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;


//for hardboiled egg and basic enemies
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
                    Debug.Log("attack!");

                    break;

                // Sunny side up
                case 1:
                    handSlotR.GetComponent<SunnyAttack>().Attack();
                    break;

                // Scrambled
                case 2:
                    break;

                // Poached
                case 3:
                    break;

                // Goon
                case 4:
                    if (rotateCoroutine != null) return;

                    swingingDown = true;

                    //this is a disgusting call, but i don't have a better solution at the moment
                    rotateCoroutine = StartCoroutine(RotateToAngle(handSlotR.transform.GetComponentInChildren<WeaponData>().swingSpeed));
                    Debug.Log("attack!");

                    break;

                // Devilled egg
                case 5:
                    if (rotateCoroutine != null) return;

                    swingingDown = true;

                    //this is a disgusting call, but i don't have a better solution at the moment
                    rotateCoroutine = StartCoroutine(RotateToAngle(handSlotR.transform.GetComponentInChildren<WeaponData>().swingSpeed));
                    Debug.Log("attack!");

                    break;

                default:
                    break;
            }
        }

        // Left-handed (secondary) attack
        else
        {
            switch (entityType)
            {
                // Hard boiled
                case 0:
                    break;

                // Sunny side up
                case 1:
                    handSlotL.GetComponent<SunnyAttack>().Attack();
                    break;

                // Scrambled
                case 2:
                    break;

                // Poached
                case 3:
                    break;

                // Scrambled
                case 4:
                    break;

                // Poached
                case 5:
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
