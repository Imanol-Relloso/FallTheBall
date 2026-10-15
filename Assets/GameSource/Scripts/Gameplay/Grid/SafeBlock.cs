using System;
using UnityEngine;

public class SafeBlock : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        GameObject grid = transform.parent.gameObject;
        GridManager.Instance.HandleGridExit(grid);
    }
}
