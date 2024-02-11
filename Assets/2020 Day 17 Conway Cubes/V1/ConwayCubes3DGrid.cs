using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class ConwayCubes3DGrid : MonoBehaviour
{
    public bool[] Cubes;
    [Range(1,10)]public int GridSize = 10;


    private void OnValidate()
    {
        if (GridSize <= 0) GridSize = 1;
        if(Mathf.Pow(GridSize, 3) != Cubes.Length)
        {
            Cubes = new bool[Mathf.RoundToInt(Mathf.Pow(GridSize, 3))];
        }
    }
    public Vector3 GetCubePos(int i)
    {
        Vector3 vec = Vector3.zero;
        //vec.x = i;
        vec.x = (i % GridSize);
        vec.y = Mathf.FloorToInt(i / (float)(GridSize* GridSize));
        vec.z = Mathf.FloorToInt(i / (float)GridSize) - (GridSize* vec.y);
        
        //vec.y = i % GridSize;
        return vec;
    }
    public int[] GetNeighbors(int inputCube)
    {
        //GridSize = 3
        //inputCube = 13
        // +- 1 = 12,14 = (1)
        // +- 3 = 10,16 = (GridSize)
        // +- 9 = 4 , 22 = (GridSize * GridSize)
        int[] neighbors = new int[6];
        for (int i = 0; i < neighbors.Length; i++)
        {
            neighbors[i] = -1;
        }
        //int MIN = 0;
        int MAX = Cubes.Length;

        //3D Plus sign
        neighbors[0] = inputCube + 1;
        neighbors[1] = inputCube - 1;
        neighbors[2] = inputCube + GridSize;
        neighbors[3] = inputCube - GridSize;
        neighbors[4] = inputCube + (GridSize * GridSize);
        neighbors[5] = inputCube - (GridSize * GridSize);
        //rest


        return neighbors;
    }
    public void SetCubeState(int cube,bool state)
    {
        if (cube >= 0 && cube <= Cubes.Length)
        Cubes[cube] = state;
    }
    public void Clear()
    {
        for (int i = 0; i < Cubes.Length; i++)
        {
            Cubes[i] = false;
        }
    }
    public bool GetCubeState(int i)
    {
        return Cubes[i];
    }
}
