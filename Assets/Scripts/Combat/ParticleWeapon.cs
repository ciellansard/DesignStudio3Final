using UnityEngine;

public class ParticleWeapon : MonoBehaviour
{
    [SerializeField]
    MeshRenderer mesh;
    [SerializeField]
    ParticleSystem particles;
    [SerializeField]
    GameObject subMesh;
    private void OnTriggerEnter(Collider other)
    {
        mesh.enabled = false;
        particles.Play();
        if (subMesh != null) subMesh.active = false;
    }
}
