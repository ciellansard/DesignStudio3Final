using System.Collections;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public Quaternion targetRotation = Quaternion.Euler(90, 0, 0);
    private Quaternion restPosition = Quaternion.Euler(0, 0, 0);
    public Coroutine rotateCoroutine;
    bool swingingDown = true;

    [SerializeField]
    private TrailRenderer trail;

    public void attack(GameObject handslot)
    {
        //restPosition = Quaternion.Euler(0, 0, 0);

        if (rotateCoroutine != null) return;

        swingingDown = true;

        //this is a disgusting call, but i don't have a better solution at the moment
        trail.enabled = true;
        trail.emitting = true;
        rotateCoroutine = StartCoroutine(RotateToAngle(handslot.transform.GetComponentInChildren<WeaponData>().swingSpeed, handslot));
    }

    private IEnumerator RotateToAngle(float swingSpeed, GameObject handslot)
    {
        //rotate down
        while (Quaternion.Angle(handslot.transform.localRotation, targetRotation) > 0.1f && swingingDown)
        {
            //Debug.Log("swinging down");
            handslot.transform.localRotation = Quaternion.RotateTowards(handslot.transform.localRotation, targetRotation, swingSpeed * Time.deltaTime);
            if (Quaternion.Angle(handslot.transform.localRotation, targetRotation) < 0.1f) swingingDown = false;
            yield return null;
        }
        trail.emitting = false;
        trail.enabled = false;
        //rotate back up to rest
        while (Quaternion.Angle(handslot.transform.localRotation, restPosition) > 0.1f)
        {
            //Debug.Log("swinging up");
            handslot.transform.localRotation = Quaternion.RotateTowards(handslot.transform.localRotation, restPosition, swingSpeed * Time.deltaTime);
            yield return null;
        }

        handslot.transform.localRotation = restPosition;

        yield return new WaitForSeconds(0.5f);

        rotateCoroutine = null;
    }
}
