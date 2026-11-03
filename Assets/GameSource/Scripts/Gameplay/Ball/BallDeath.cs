using System;
using UnityEngine;
using DG.Tweening;

public class BallDeath : MonoBehaviour
{
    private BallMovement  ballMovement;
    
    private Vector3 ballScale;
    private Collider coll;
    private Rigidbody rb;
    
    [Header("Death settings")]
    [SerializeField] 
    private float yScale = 0.1f;
    [SerializeField]
    private float deathTime = 0.5f;
    [SerializeField]
    private Ease deathEase = Ease.OutQuad;
    private void OnEnable()
    {
        Init();
    }

    private void Init()
    {
        ballMovement = transform.GetComponent<BallMovement>();
        ballScale = transform.localScale;
        coll = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
    }

    public void Death()
    {
        //Congelo movimiento
        ballMovement.FreezeMovement();
        
        //Desactiva la colision y gravedad para que la animacion funcione 
        coll.enabled = false;
        rb.useGravity = false;
        
        //Animacion de muerte
        transform.DOScaleY(yScale, deathTime).SetEase(deathEase);
        transform.DOMoveY(transform.position.y - ballScale.y/2, deathTime).SetEase(deathEase);
    }

    public void Reset()
    {
        //Reseteo la escala
        transform.localScale = ballScale;
        
        //Activa la colision y gravedad
        coll.enabled = true;
        rb.useGravity = true;
        
        //Reinicio la bola
        ballMovement.ResetBall();
        ballMovement.CanMove();
    }

}
