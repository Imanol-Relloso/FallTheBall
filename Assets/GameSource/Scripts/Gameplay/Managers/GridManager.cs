using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance { get; private set; }
    
    [SerializeField] 
    private GridGenerator gridGenerator;
    
    private List<GameObject> _grids;
    private List<GameObject> _processedGrids;

    private int _skippedGrids = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        _grids = new List<GameObject>();
        _processedGrids = new List<GameObject>();
    }

    public void StartGeneration()
    {
        gridGenerator.Init();
    }

    public void AddGrid(GameObject grid)
    {
        _grids.Add(grid);
    }
    
    public void HandleGridExit(GameObject grid)
    {
        if (!_grids.Contains(grid))
            return;

        if (_processedGrids.Contains(grid))
            return;
        
        _processedGrids.Add(grid);
        
        if (_skippedGrids < gridGenerator.skipToGrids)
        {
            _skippedGrids++;
            return;
        }

        DeleteFirstGrid();
    }
    public void GenerateGrid()
    {
        gridGenerator.GenerateGrid();
    }
    public void DeleteFirstGrid()
    {
        Destroy(_grids[0]);
        _grids.RemoveAt(0);
        
        GenerateGrid();
    }
    
    public void DeleteActualGrid()
    {
        Destroy(GetActualGrid());
        _grids.RemoveAt(GetActualGridIndex());
        
        GenerateGrid();
    }

    public GameObject GetActualGrid()
    {
        return _grids[gridGenerator.skipToGrids];
    }

    private int GetActualGridIndex()
    {
        return gridGenerator.skipToGrids;
    }

    public void ResetGrids()
    {
        HashSet<GameObject> allGrids = new HashSet<GameObject>(_grids);
        allGrids.UnionWith(_processedGrids);

        foreach (GameObject grid in allGrids)
        {
            if (grid != null)
                Destroy(grid);
        }

        _grids.Clear();
        _processedGrids.Clear();

        _skippedGrids = 0;
    }
}
