using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictionaryConwayCubes : MonoBehaviour
{
    #region Steps
    /// <summary>
    /// Data sets CubesStates,CachedCubesStates,CubesActiveNeighborCount
    /// Spawn randomcubes 
    /// Calculate CubesActiveNeighborCount by looping through all cubes in CubesStates and getting their active neighbors and put the total in CubesActiveNeighborCount
    ///  
    /// 
    /// for each pos in CubesActiveNeighborCount set the int
    /// 
    /// Clone CubesStates to CachedCubesStates 
    /// 
    /// 
    /// </summary>
        #endregion


    #region Hidden Refs
    public DictionaryConwayCubesData data;
    private void OnValidate()
    {
        if (data == null) data = FindObjectOfType<DictionaryConwayCubesData>();
    }
    #endregion
    //[Header("Settings")]

    public void Step()
    {
        OnDictionaryChange();
    }
    public void ClearDictionary()
    {
        data.ClearDictionary();
        OnDictionaryChange();
    }
    public void SpawnRandom()
    {
       int count = Random.Range(1, 10);
        int size = 5 + (int)(data.CubesStates.Count * 0.1f);
        for (int i = 0; i < count; i++)
        {
            data.SetCube(new Vector3(Random.Range(-size, size), Random.Range(-size, size), Random.Range(-size, size)), true);
        }
        OnDictionaryChange();
    }
    public void OnDictionaryChange()
    {
        data.CalculateBounds();
    }
    private void OnDrawGizmos()
    {
        if (data == null) return;
        List<Vector3> enabledCubes = new List<Vector3>();
        enabledCubes = data.GetEnabledCubes();
        if (enabledCubes.Count > 0)
        {
            #region Render Enabled Cubes
            Gizmos.color = Color.white;
            foreach (Vector3 cub in enabledCubes)
            {
                Gizmos.DrawCube(cub, Vector3.one);
            }
            #endregion
            #region Render Bounds
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(data.Bounds[2], data.Bounds[3]+(Vector3.one));//0 Min, 1 Max, 2 Average, 3 Range
            #endregion
        }

    }

    
}