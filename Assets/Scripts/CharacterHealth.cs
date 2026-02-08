using Unity.VisualScripting;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    [SerializeField]
    private int maxHealth;
    [SerializeField]
    public int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("hit something");
        if (other.gameObject.CompareTag("Harmful"))
        {
            //Debug.Log("hit a harmful object");
            if (other.gameObject.TryGetComponent<WeaponData>(out WeaponData weapon)) currentHealth -= weapon.damage;
            //Debug.Log(weapon.damage);
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        }
    }
    
}
