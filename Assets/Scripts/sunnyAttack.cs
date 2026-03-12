using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

//for sunnys egg pepper and salt attacks
public class SunnyAttack : MonoBehaviour
{
    [SerializeField]
    //private GameObject handSlot;
    public Camera playerCam;
    bool reloadProjectile = false;

    public GameObject projectile;
    private GameObject pCopy;
    float pHeight;

    public Coroutine rechargeProjectile;

    public void Awake()
    {
        projectile.SetActive(false);
    }
     public void Attack()
    {
        if (reloadProjectile == false)//makes it so only one projectile is shot at once
        {
            reloadProjectile = true;
           
            pCopy = Instantiate(projectile);

            pCopy.transform.position = this.transform.position;
          
            pCopy.GetComponent<Rigidbody>().isKinematic = false;
            pCopy.SetActive(true); //set visible
            pCopy.GetComponent<Rigidbody>().linearVelocity = playerCam.transform.forward * 20; //launch direction and speed/power

            rechargeProjectile = StartCoroutine(RechargeWait(projectile.GetComponent<WeaponData>().swingSpeed)); //wait until ability reloads, value is swinging speed on weapon's prefab
        }
            
        Debug.Log("Sunny side up egg attacks with " + projectile.name + "!");
    }

    private void Update()
    {
        if (pCopy) pHeight = pCopy.transform.position.y;
        if (pHeight < -2) { Destroy(pCopy); }
    }

    private IEnumerator RechargeWait(float duration)
    {
        yield return new WaitForSeconds(duration);
        reloadProjectile = false;
    }

}
