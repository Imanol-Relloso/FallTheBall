using System;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    [Header("Transition Settings")] 
    [SerializeField]
    private Vector3 initialRot;
    [SerializeField]
    private Vector3 initialPos;
    [SerializeField]
    private Vector3 finalRot;
    [SerializeField]
    private float transitionTime = 0.5f;
    [SerializeField]
    private Ease transitionEase = Ease.Linear;
    
    [Header("Gameplay Settings")]
    [SerializeField] 
    private GameObject ball;    
    [SerializeField] 
    private float offset;

    private bool isPlaying = false;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        ResetCamera();
    }

    private void FixedUpdate()
    {
        if (isPlaying)
        {
            transform.position = new Vector3(
                transform.position.x, 
                ball.transform.position.y + offset, 
                transform.position.z);
        }
    }

    public void GameTransition()
    {
        transform.DORotate(finalRot, transitionTime) .SetEase(Ease.Linear).onComplete += () => isPlaying = true;
    }
    
    public void PlayAgainTransition()
    {
        isPlaying = false;
        
        transform.DOMove(initialPos, transitionTime) .SetEase(Ease.Linear).onComplete += () => isPlaying = true;
    }

    public void ResetCamera()
    {
        transform.position = initialPos;
        transform.rotation = Quaternion.Euler(initialRot);
        isPlaying = false;
    }
}
