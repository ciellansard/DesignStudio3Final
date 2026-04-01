using UnityEngine;

public class tumbleSound : MonoBehaviour
{
    private SoundManager soundManager;

    private void Awake()
    {
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        soundManager.PlaySound(soundManager.tumble);
    }

}
