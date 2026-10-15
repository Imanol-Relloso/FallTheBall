using System;
using UnityEngine;

public class BallSpeed : MonoBehaviour
{
    private Rigidbody _rb;
    private MeshRenderer _meshRenderer;
    
    [Header("Speed settings")]
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float resetSpeed;
    
    [Header("Color settings")]
    [SerializeField]
    private Color normalColor = Color.white;
    [SerializeField]
    private Color SupersonicColor = Color.red;

    private bool _supersonic = false;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void FixedUpdate()
    {
        SpeedControl();
    }

    private void SpeedControl()
    {
        if(_rb.linearVelocity.y <= -maxSpeed)
        {
            Vector3 vector3 = _rb.linearVelocity;
            vector3.y = -maxSpeed;
            _rb.linearVelocity = vector3;

            IsMaxSpeed();
        }
    }

    private void IsMaxSpeed()
    {
        if(_supersonic) return;
            
        _supersonic = true;
        _meshRenderer.material.color = SupersonicColor;
    }

    private void LostSupersonic()
    {
        _supersonic = false;
        _meshRenderer.material.color = normalColor;
        
        Vector3 vector3 = _rb.linearVelocity;
        vector3.y = -resetSpeed;
        _rb.linearVelocity = vector3;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_supersonic && collision.gameObject.CompareTag("Floor"))
        {
            GridManager.Instance.DeleteActualGrid();
            
            LostSupersonic();
        }
        else if(!_supersonic && collision.gameObject.CompareTag("Floor"))
        {
            GameManager.Instance.GameOver();
        }
    }
}
