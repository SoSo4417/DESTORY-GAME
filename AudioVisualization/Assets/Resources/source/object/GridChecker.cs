using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridChecker : MonoBehaviour
{
    public bool[] grid;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetGridNum(int num)
    {
        grid = new bool[num];
    }

    public bool CheckGrid(int num)
    {
        return grid[num];
    }

    public void OnGrid(int num)
    {
        grid[num] = true;
    }

    public void OffGrid(int num)
    {
        grid[num] = false;
    }

    public void ResetGrid()
    {
        for(int i = 0; i < grid.Length; i++)
        {
            grid[i] = false;
        }
    }
}
