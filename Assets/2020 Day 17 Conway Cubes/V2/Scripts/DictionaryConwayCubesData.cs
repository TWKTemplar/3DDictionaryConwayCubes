using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictionaryConwayCubesData : MonoBehaviour
{

    public Dictionary<Vector3, bool> Cubes = new Dictionary<Vector3, bool>();
    public List<Vector3> EnabledCubes = new List<Vector3>();

    public Dictionary<Vector3, int> ActiveCubes = new Dictionary<Vector3, int>();
    //dic of all cubes with more than 1 enabled cube around them

    public Vector3[] Bounds = new Vector3[4];//0 Min, 1 Max, 2 Average, 3 Range
    //Only contains cubes with 1 true neighbor near them, including them.

    public void ApplyRulesToSpawnCubesInto()
    {
        Cubes.Clear();
        //Clone ActiveCubes to Cubes
        if (ActiveCubes.Count > 50000) return;
        if (ActiveCubes.Count != 0)
        {
            foreach (var cubePair in ActiveCubes)
            {
                if(cubePair.Value == 3)
                {
                    Cubes.Add(cubePair.Key, true);
                }
            }
        }

    }

    public void AddToActiveCubes(Vector3 cube)
    {
        if(ActiveCubes.ContainsKey(cube))
        {
            ActiveCubes[cube]++;
        }
        else
        {
            ActiveCubes.Add(cube, 1);
        }
    }
    public void CalculateActiveCubes()
    {
        ActiveCubes.Clear();
        if (Cubes.Count != 0)
        {
            foreach (var cube in Cubes)
            {
               foreach(var cubeNeighbor  in GetNeighborsKeys(cube.Key))
               {
                    AddToActiveCubes(cubeNeighbor);
               }
            }
        }
    }
    public bool GetCube(Vector3 cube)
    {
        if (!Cubes.ContainsKey(cube)) Cubes.Add(cube, false);
        return Cubes[cube];
    }
    public void SetCube(Vector3 cube, bool state)
    {
        if (!Cubes.ContainsKey(cube)) Cubes.Add(cube, state);
        Cubes[cube] = state;
    }
    public void RemoveCubeFromDictionary(Vector3 cube)
    {
        Cubes.Remove(cube);
    }
    public void ClearCubesDictionary()
    {
        Cubes.Clear();
    }
    public void CalculateBounds()
    {
        if (Cubes.Count != 0)
        {
            foreach (var val in Cubes)
            {
                Bounds[0].x = Mathf.Min(Bounds[0].x, val.Key.x);
                Bounds[0].y = Mathf.Min(Bounds[0].y, val.Key.y);
                Bounds[0].z = Mathf.Min(Bounds[0].z, val.Key.z);
                Bounds[1].x = Mathf.Max(Bounds[1].x, val.Key.x);
                Bounds[1].y = Mathf.Max(Bounds[1].y, val.Key.y);
                Bounds[1].z = Mathf.Max(Bounds[1].z, val.Key.z);
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
    public void CalcEnabledDisabledCubes()
    {
        EnabledCubes.Clear();
        if (Cubes.Count != 0)
        {
            foreach (var val in Cubes)
            {
                if(val.Value)EnabledCubes.Add(val.Key);
            }
        }
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
