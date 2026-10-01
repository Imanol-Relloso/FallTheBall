using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ball : MonoBehaviour
{
    private BallInputSystem  ballInputSystem;
    
    private bool isMoving = false;

    private Vector2 postMove;

    [SerializeField] 
    private float moveTime = 0.1f;

    private void Awake()
    {
        ballInputSystem = new BallInputSystem();
    }

    private void OnEnable()
    {
        InputManager.Instance.OnKeyInput += PressMove;
        InputManager.Instance.OnDragEnd += HandleDragEnd;
    }

    private void HandleDragEnd(Vector2 dragDir)
    {
        Vector2 normalizedDragDir = dragDir.normalized;

        if (Mathf.Abs(normalizedDragDir.x) > Mathf.Abs(normalizedDragDir.y))
        {
            if(normalizedDragDir.x > 0)
                PressMove(Vector2.right);
            else
                PressMove(Vector2.left);
        }
        else
        {
            if (normalizedDragDir.y > 0)
                PressMove(Vector2.up);
            else
                PressMove(Vector2.down);
        }
    }

    private void OnDisable()
    {
        InputManager.Instance.OnKeyInput -= PressMove;
        InputManager.Instance.OnDragEnd -= HandleDragEnd;
    }

    private void PressMove(Vector2 dir)
    {
        if (isMoving)
            saveInput(dir);
        else
            StartCoroutine(Move(dir));
    }

    private void saveInput(Vector2 dir)
    {
        postMove = dir;
    }
    
    private void tryPostMove()
    {
        if (postMove != Vector2.zero)
        {
            StartCoroutine(Move(postMove));
            postMove = Vector2.zero;
        }
    }

    private IEnumerator Move(Vector2 dir)
    {
        isMoving = true;
        Vector2 finalPos = new Vector2(transform.position.x + dir.x, transform.position.z + dir.y);

        float t = 0;

        while (t <= moveTime)
        {
            Vector3 posToMove = Vector3.Lerp(transform.position,
                new Vector3(finalPos.x, transform.position.y, finalPos.y),
                t / moveTime);
            transform.position = posToMove;

            t += Time.deltaTime;
            
            yield return null;
        }
        
        transform.position = new Vector3(finalPos.x, transform.position.y, finalPos.y);
        isMoving = false; 
        
        tryPostMove();
    }
}
