using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public List<GameObject> pathCells = new List<GameObject>();
    public List<GameObject> towerCells = new List<GameObject>();

    void Start()
    {
        
        foreach (Transform child in transform)
        {
           
            if (child.name.StartsWith("Row"))
            {
                
                foreach (Transform cell in child)
                {
                    if (cell.tag == "Tower")
                    {
                        towerCells.Add(cell.gameObject);
                    }
                }
            }
            
            else if (child.name == "Path")
            {
                
                foreach (Transform cell in child)
                {
                    if (cell.tag == "Path")
                    {
                        pathCells.Add(cell.gameObject);
                    }
                }
            }
        }
    }
}
