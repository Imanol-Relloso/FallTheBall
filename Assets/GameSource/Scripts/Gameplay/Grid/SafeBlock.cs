using System;
using UnityEngine;

public class SafeBlock : MonoBehaviour
{
    private GridGenerator _gridGenerator;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _gridGenerator = transform.GetComponentInParent<GridGenerator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _gridGenerator.GenerateGrid();
    }

    private void OnTriggerExit(Collider other)
    {
        if (_gridGenerator.skipToGrids > 0)
            _gridGenerator.skipToGrids--;
        else
            _gridGenerator.DeleteFirstGrid();
    }
}
