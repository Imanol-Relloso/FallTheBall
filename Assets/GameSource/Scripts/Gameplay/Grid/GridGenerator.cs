using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] 
    private GameObject floor;
    [SerializeField]
    private GameObject safeBlock;

    [SerializeField]
    private float safeBlockProbability = 0.2f;
    
    private List<GameObject> grids;

    private int gridX = 3;
    private int gridZ = 3;

    [SerializeField]
    private float separationY = 5f; 
    
    [SerializeField]
    private float initialGridY = -4f;
    
    [SerializeField]
    private int initGridCuantity = 4;
    
    [SerializeField]
    public int skipToGrids = 3;

    private void Start()
    {
        grids = new List<GameObject>();
        
        Init();
    }

    public void Init()
    {
        for (int i = 0; i < initGridCuantity; i++)
        {
            GenerateGrid();
        }
    }

    public void GenerateGrid()
    {
        GameObject grid = new GameObject();
        grid.transform.SetParent(transform);
        
        grid.transform.localPosition = new Vector3(0, initialGridY, 0);
        initialGridY -= separationY;
        
        int totalCells = gridX * gridZ;

        // Mínimos según el tamaño
        int minEmpties = Mathf.Max(1, totalCells / 9);
        int minfloors = Mathf.Max(5, (totalCells / 9) * 5);

        List<GameObject> cells = new List<GameObject>();

        //Bloques que van a salir en la grid
        for (int i = 0; i < totalCells; i++)
        {
            if(i < minEmpties)
            {
                cells.Add(safeBlock);
            }
            else if(i < minfloors + minEmpties)
            {
                cells.Add(floor);
            }
            else
            {
                if(Random.Range(0f, 1f) < safeBlockProbability)
                    cells.Add(safeBlock);
                else
                    cells.Add(floor);
            }
        }

        //Aleatorizas orden de los bloques
        for (int i = 0; i < cells.Count; i++)
        {
            int randomIndex = Random.Range(i, cells.Count);

            GameObject temp = cells[i];
            cells[i] = cells[randomIndex];
            cells[randomIndex] = temp;
        }
        

        float startX = -(gridX / 2);
        float startZ = -(gridZ / 2);
        int index = 0;

        for (int x = 0; x < gridX; x++)
        {
            for (int z = 0; z < gridZ; z++)
            {
                GameObject cell = Instantiate(cells[index], grid.transform);

                cell.transform.localPosition = new Vector3(startX + x, 0, startZ + z);

                index++;
            }
        }

        grids.Add(grid);
    }

    public void DeleteFirstGrid()
    {
        Destroy(grids[0]);
        grids.RemoveAt(0);
    }
    
    

}
