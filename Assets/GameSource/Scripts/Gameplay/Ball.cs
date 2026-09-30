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
        ballInputSystem.Player.Move.performed += pressMove;
        ballInputSystem.Player.Move.Enable();
    }

    private void OnDisable()
    {
        ballInputSystem.Player.Move.Disable();
    }

    private void pressMove(InputAction.CallbackContext ctx)
    {
        if (isMoving)
            saveInput(ctx.ReadValue<Vector2>());
        else
            StartCoroutine(Move(ctx.ReadValue<Vector2>()));
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
