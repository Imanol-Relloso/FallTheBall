using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ball : MonoBehaviour
{
    private bool _isMoving = false;

    private Vector2 _postMove;
    
    private int _maxX = 1;
    private int _minX = -1;
    private int _maxZ = 1;
    private int _minZ = -1;

    [SerializeField] 
    private float moveTime = 0.1f;



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
        if (_isMoving)
            SaveInput(dir);
        else
            StartCoroutine(Move(dir));
    }

    private void SaveInput(Vector2 dir)
    {
        _postMove = dir;
    }
    
    private void TryPostMove()
    {
        if (_postMove != Vector2.zero)
        {
            StartCoroutine(Move(_postMove));
            _postMove = Vector2.zero;
        }
    }

    private IEnumerator Move(Vector2 dir)
    {
        _isMoving = true;
        Vector2 finalPos = new Vector2(Mathf.Clamp(transform.position.x + dir.x, _minX, _maxX) ,Mathf.Clamp(transform.position.z + dir.y, _minZ, _maxZ));

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
        _isMoving = false; 
        
        TryPostMove();
    }
}
