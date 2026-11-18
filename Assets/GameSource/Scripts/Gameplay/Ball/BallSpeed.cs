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
    
    [Header("Supersonic settings")]
    [SerializeField]
    private Material normalMat;
    [SerializeField]
    private Material supersonicMat;
    [SerializeField]
    private GameObject fireParticles;
    [SerializeField]
    private int supersonicGrids;
    
    private GameObject actualFireParticles;
    
    private bool _supersonic = false;
    private float lastDistance = 0.0f;
    
    private BallDeath ballDeath;

    private int passedGrids;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
        ballDeath = GetComponent<BallDeath>();
    }

    private void FixedUpdate()
    {
        SpeedControl();
    }

    private void SpeedControl()
    {
        if(ballDeath.IsDeath()) return;
        
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
        if(passedGrids < supersonicGrids) return;
        
        _supersonic = true;
        _meshRenderer.material = supersonicMat;
        
        actualFireParticles = Instantiate(fireParticles, transform.position + new Vector3(0,0.15f,0), Quaternion.identity);
        actualFireParticles.transform.parent = transform;
    }

    private void LostSupersonic()
    {
        _supersonic = false;
        _meshRenderer.material = normalMat;
        supersonicGrids = 0;
        
        Destroy(actualFireParticles);
        
        Vector3 vector3 = _rb.linearVelocity;
        vector3.y = -resetSpeed;
        _rb.linearVelocity = vector3;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_supersonic && collision.gameObject.GetComponent<Floor>())
        {
            Transform grid = GridManager.Instance.GetActualGrid().transform;

            for (int i = 0; i < grid.childCount; i++)
            {
                grid.GetChild(i).GetComponent<Floor>()?.SpawnParticle();
            }
            
            GridManager.Instance.DeleteActualGrid();
            
            LostSupersonic();
        }
        else if(!_supersonic && collision.gameObject.GetComponent<Floor>())
        {
            GameManager.Instance.GameOver();
            transform.position = collision.gameObject.transform.position + new Vector3(0,0.15f,0);
        }
    }
    
    public bool GetSupersonic()
    {
        return _supersonic;
    }
    
    public void SetMaxSpeed(float value)
    {
        maxSpeed = value;
    }
    public void SetMinSpeed(float value)
    {
        resetSpeed = value;
    }
    public void SetSpeed(float value)
    {
        _rb.linearVelocity = new Vector3(0f, value, 0f);
    }
    public void AddPassedGrids()
    {
        passedGrids++;
    }

    public void SetSupersonicGrid(int value)
    {
        supersonicGrids = value;
    }
}
