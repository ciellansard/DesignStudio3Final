using UnityEngine;

public class ParticleWeapon : MonoBehaviour
{
    [SerializeField]
    MeshRenderer mesh;
    [SerializeField]
    ParticleSystem particles;
    [SerializeField]
    GameObject subMesh;

    private SoundManager soundManager;
    bool playDaThing = true;

    private void Awake()
    {
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        mesh.enabled = false;
        particles.Play();

        if (subMesh != null) subMesh.active = false;

        if (playDaThing)
        {
            soundManager.PlaySound(soundManager.impacts);
            playDaThing = false;
        }

    }
}
