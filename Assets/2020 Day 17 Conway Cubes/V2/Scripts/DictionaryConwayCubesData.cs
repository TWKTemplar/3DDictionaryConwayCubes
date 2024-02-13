using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictionaryConwayCubesData : MonoBehaviour
{

    [Header("Debug Read Only")]
    public int NumberOfCubes = 0;
    [Header("Internal")]
    public HashSet<Vector3> Cubes = new HashSet<Vector3>();
    public Dictionary<Vector3, int> FertilityMap = new Dictionary<Vector3, int>();
    public Vector3[] Bounds = new Vector3[4];//0 Min, 1 Max, 2 Average, 3 Range

    public void ApplyRulesToCubes(int gameOfLifeSurviveValue, int MaxCubes = 50000)
    {
        Cubes.Clear();
        if (FertilityMap.Count > MaxCubes) FertilityMap.Clear();
        if (FertilityMap.Count == 0)
        {
            NumberOfCubes = 0;
            return;
        }
        foreach (var cubePair in FertilityMap)
        {
            if(cubePair.Value == gameOfLifeSurviveValue) Cubes.Add(cubePair.Key);
        }

    }

    public void CalculateFertilityMap()
    {
        FertilityMap.Clear();
        if (Cubes.Count == 0) return;
        foreach (var cube in Cubes)
        {
            foreach (var cubeNeighbor in GetNeighborsKeys(cube))
            {
                if (FertilityMap.ContainsKey(cube)) FertilityMap[cube]++;
                else FertilityMap.Add(cube, 1);
            }
        }
        NumberOfCubes = FertilityMap.Count;
    }
    public void SetCube(Vector3 cube)
    {
        if (!Cubes.Contains(cube)) Cubes.Add(cube);
    }
    public void ClearCubes()
    {
        Cubes.Clear();
    }
    
    public Vector3[] GetNeighborsKeys(Vector3 cube)
    {
        Vector3[] n = new Vector3[27];//9+9+9
        Vector3 vec = Vector3.zero;
        //Version 1
        int i = 0;
        for (int x = -1; x != 2; x++)
        {
            for (int y = -1; y != 2; y++)
            {
                for (int z = -1; z != 2; z++)
                {
                    vec = cube;
                    vec.x += x;
                    vec.y += y;
                    vec.z += z;
                    n[i] = vec;
                    i++;
                }
            }
        }
        return n;
    }
    #region Debug Only
    public void CalculateBounds()// 0 Min, 1, Max, 2 Avg, 3 Range
    {
        if (Cubes.Count != 0)
        {
            foreach (var val in Cubes)
            {
                Bounds[0].x = Mathf.Min(Bounds[0].x, val.x);
                Bounds[0].y = Mathf.Min(Bounds[0].y, val.y);
                Bounds[0].z = Mathf.Min(Bounds[0].z, val.z);
                Bounds[1].x = Mathf.Max(Bounds[1].x, val.x);
                Bounds[1].y = Mathf.Max(Bounds[1].y, val.y);
                Bounds[1].z = Mathf.Max(Bounds[1].z, val.z);
            }
            Bounds[2] = AverageVector3(Bounds[0], Bounds[1]);
            Bounds[3] = RangeVector3(Bounds[0], Bounds[1]);
        }
        else
        {
            Bounds[0] = Vector3.zero;
            Bounds[1] = Vector3.zero;
            Bounds[2] = Vector3.zero;
            Bounds[3] = Vector3.zero;
        }
    }
    #endregion

    #region Math spam
    private Vector3 AverageVector3(Vector3 a, Vector3 b)
    {
        Vector3 vec = Vector3.zero;
        vec.x = (a.x + b.x) * 0.5f;
        vec.y = (a.y + b.y) * 0.5f;
        vec.z = (a.z + b.z) * 0.5f;
        return vec;
    }
    private Vector3 RangeVector3(Vector3 a, Vector3 b)
    {
        Vector3 vec = Vector3.zero;
        vec.x = (Mathf.Abs(a.x) + Mathf.Abs(b.x));
        vec.y = (Mathf.Abs(a.y) + Mathf.Abs(b.y));
        vec.z = (Mathf.Abs(a.z) + Mathf.Abs(b.z));
        return vec;
    }

    #endregion
}
