using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridnumOfObj : MonoBehaviour
{
    public int GridNum;
    private GameObject Gr_M;
    private GridChecker gr_m;

    // Start is called before the first frame update
    void Start()
    {
        Gr_M = GameObject.Find("GridManager");
        gr_m = Gr_M.GetComponent<GridChecker>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetGridNum(int num)
    {
        GridNum = num;
    }

    public int GetGridNum()
    {
        return GridNum;
    }

    public void DestroyAndOff()
    {
        gr_m.OffGrid(GridNum);
    }
}
