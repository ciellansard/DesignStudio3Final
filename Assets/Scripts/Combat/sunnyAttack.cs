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

    private SoundManager soundManager;

    public void Awake()
    {
        //restPosition = this.transform.rotation;
        projectile.SetActive(false);
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
    }
     public void Attack()
    {
        if (reloadProjectile == false)
        {
            reloadProjectile = true;
           
            pCopy = Instantiate(projectile);
            pCopy.transform.position = this.transform.position;
          
            pCopy.GetComponent<Rigidbody>().isKinematic = false;
            pCopy.SetActive(true);
            pCopy.GetComponent<Rigidbody>().linearVelocity = playerCam.transform.forward * 20;

            soundManager.PlaySound(soundManager.wind);
            soundManager.FadeOutSfx(1);

            rechargeProjectile = StartCoroutine(RechargeWait(projectile.GetComponent<WeaponData>().swingSpeed));
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
