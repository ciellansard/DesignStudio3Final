using Unity.VisualScripting;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    [SerializeField]
    private int maxHealth;
    [SerializeField]
    public int currentHealth;
    [SerializeField]
    private EggBarIcons hudScript;
    private bool isHit;

    private void Awake()
    {
        currentHealth = maxHealth;
        if (hudScript  != null ) hudScript.SetMaxHP(maxHealth);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit something");
        if (other.gameObject.CompareTag("Harmful"))
        {
            if (isHit) return;
            isHit = true;
            //Debug.Log("hit a harmful object");
            if (other.gameObject.TryGetComponent<WeaponData>(out WeaponData weapon)) currentHealth -= weapon.damage;
            if (other.gameObject.TryGetComponent<ParticleSystem>(out ParticleSystem particleSystem)) {
                particleSystem.Play();
                other.gameObject.GetComponent<MeshRenderer>().enabled = false;
            }

            //Debug.Log(weapon.damage);
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            if (hudScript != null) hudScript.UpdateHealth(currentHealth);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Harmful"))
        {
            isHit = false;
        }
    }

}
