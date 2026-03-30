using Unity.VisualScripting;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    [SerializeField]
    public int maxHealth;
    [SerializeField]
    public int currentHealth;
    [SerializeField]
    private EggBarIcons hudScript;
    private bool isHit;
    [SerializeField]
    private SoundManager soundManager;

    private GameObject CurrentGameObject;
    private void Awake()
    {
        currentHealth = maxHealth;
        if (gameObject.CompareTag("Player"))
        {
            hudScript.SetMaxHP(maxHealth);
        }
    }

    private void Update()
    {
        if (gameObject.CompareTag("Player"))
        {
            if (currentHealth <= 0)
            {
                soundManager.PlaySound(soundManager.loss);
            }
        }
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
            if (gameObject.TryGetComponent<ParticleSystem>(out ParticleSystem particles)) particles.Play();

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

    private void OnParticleCollision(GameObject other)
    {
        //this just makes sure enemies can only be hurt by a single cloud once
        if (CurrentGameObject == other.gameObject) return;
       
        CurrentGameObject = other.gameObject;


        if (other.gameObject.TryGetComponent<WeaponData>(out WeaponData weapon)) currentHealth -= weapon.damage;
        if (gameObject.TryGetComponent<ParticleSystem>(out ParticleSystem particles)) particles.Play();
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        if (hudScript != null) hudScript.UpdateHealth(currentHealth);
    }

}
