using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConwayCubes : MonoBehaviour
{
    private ConwayCubes3DGrid conwayCubes3DGrid;
    public int TargetCube;

    private void OnValidate()
    {
        if (conwayCubes3DGrid == null) conwayCubes3DGrid = GetComponent<ConwayCubes3DGrid>();
    }
    public void Step()
    {
       var neighbors = conwayCubes3DGrid.GetNeighbors(TargetCube);
        for (int i = 0; i < neighbors.Length; i++)
        {
            conwayCubes3DGrid.SetCubeState(neighbors[i], true); 
        }
    }
    public void Clear()
    {
        conwayCubes3DGrid.Clear();
    }
}
