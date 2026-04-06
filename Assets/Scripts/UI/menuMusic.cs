using UnityEngine;

public class menuMusic : MonoBehaviour
{
    private SoundManager soundManager;
    public bool goAgain = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
       // soundManager.PlayMusic(soundManager.beautifulJazz);
    }

}
