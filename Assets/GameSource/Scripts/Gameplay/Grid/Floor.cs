using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField]
    private GameObject destroyParticle;

    public void SpawnParticle()
    {
        Instantiate(destroyParticle, transform.position, transform.rotation);
    }

}
