using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField]
    private GameObject destroyParticle;

    public void SpawnParticle()
    {
        GameObject particle = Instantiate(destroyParticle, transform.position, transform.rotation);
        Destroy(particle, 2f);
    }

}
