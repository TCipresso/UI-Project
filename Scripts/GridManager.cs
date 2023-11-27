using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public List<GameObject> PathCells = new List<GameObject>();
    public List<GameObject> TowerCells = new List<GameObject>();
    private List<Vector2> PlacedTowerPos = new List<Vector2>();

    void Awake()
    {
        foreach (Transform child in transform)
        {
            if (child.name.StartsWith("Row"))
            {
                foreach (Transform cell in child)
                {
                    if (cell.tag == "Tower")
                    {
                        TowerCells.Add(cell.gameObject);
                    }
                }
            }
            else if (child.name == "Path")
            {
                foreach (Transform cell in child)
                {
                    if (cell.tag == "Path")
                    {
                        PathCells.Add(cell.gameObject);
                    }
                }
            }
        }
    }

    public Cell GetCellAtPosition(Vector2 position)
    {
        
        foreach (GameObject towerCell in TowerCells)
        {
            if (Vector2.Distance(towerCell.transform.position, position) < 0.1f)
            {
                return towerCell.GetComponent<Cell>();
            }
        }
        return null;
    }

    public Vector2 GetNearestGridPoint(Vector2 position)
    {
        float CellSize = 1.0f;
        float xOffset = 0.5f * CellSize;
        float yOffset = 0.5f * CellSize;

        float x = Mathf.Round((position.x - xOffset) / CellSize) * CellSize + xOffset;
        float y = Mathf.Round((position.y - yOffset) / CellSize) * CellSize + yOffset;
        return new Vector2(x, y);
    }

    public bool IsValidPlacement(Vector2 GridPos)
    {
        foreach (GameObject TowerCell in TowerCells)
        {
            Cell cell = TowerCell.GetComponent<Cell>();
            if (cell == null) continue; 

            Vector2 TowerCellPos = new Vector2(TowerCell.transform.position.x, TowerCell.transform.position.y);

            if (Vector2.Distance(TowerCellPos, GridPos) < 0.1f)
            {
                return !cell.hasTower;
            }
        }
        return false;
    }

    public bool IsPositionOccupied(Vector2 GridPos)
    {
        
        foreach (Vector2 Pos in PlacedTowerPos)
        {
            if (Vector2.Distance(Pos, GridPos) < 0.1f)
            {
                return true;
            }
        }
        return false;
    }
}
