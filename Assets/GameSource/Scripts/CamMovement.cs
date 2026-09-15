using UnityEngine;

public class CanMovement : MonoBehaviour
{
    [SerializeField] 
    private GameObject ball;    
    [SerializeField] 
    private float offset;
    void Update()
    {
        transform.position = new Vector3(
            transform.position.x, 
            ball.transform.position.y + offset, 
            transform.position.z);
    }
}
