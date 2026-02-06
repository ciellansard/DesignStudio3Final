using TMPro;
using Unity.Mathematics;
using UnityEngine;
using System.Collections;


public class AttackControl : MonoBehaviour
{
    [SerializeField]
    private GameObject handSlot;
    public Quaternion targetRotation = Quaternion.Euler(90,0,0);
    private Coroutine rotateCoroutine;
    bool swingingDown = true;

    public void Attack()
    {
        if (rotateCoroutine != null) return;

        swingingDown = true;

        //this is a disgusting call, but i don't have a better solution at the moment
        rotateCoroutine = StartCoroutine(RotateToAngle(handSlot.transform.GetComponentInChildren<WeaponData>().swingSpeed));
        Debug.Log("attack!");
    }

    private IEnumerator RotateToAngle(float swingSpeed)
    {
        //rotate down
        while (Quaternion.Angle(handSlot.transform.localRotation, targetRotation) > 0.1f && swingingDown)
        {
            //Debug.Log("swinging down");
            handSlot.transform.localRotation = Quaternion.RotateTowards(handSlot.transform.localRotation, targetRotation, swingSpeed * Time.deltaTime);
            if (Quaternion.Angle(handSlot.transform.localRotation, targetRotation) < 0.1f) swingingDown = false;
            yield return null;
        }
        //rotate back up to rest
        while (Quaternion.Angle(handSlot.transform.localRotation, quaternion.identity) > 0.1f)
        {
            //Debug.Log("swinging up");
            handSlot.transform.localRotation = Quaternion.RotateTowards(handSlot.transform.localRotation, Quaternion.identity, swingSpeed * Time.deltaTime);
            yield return null;
        }

        handSlot.transform.localRotation = Quaternion.identity;
        rotateCoroutine = null;
    }
}
