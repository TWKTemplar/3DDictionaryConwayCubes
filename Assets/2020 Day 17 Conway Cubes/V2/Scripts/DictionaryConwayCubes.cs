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
    [Header("Colors")]
    public Color StartingColor = Color.white;
    public Color EndingColor = Color.red;

    [Header("Settings")]
    public int GameOfLifeSurviveValue = 4;
    public int MaxCubes = 50000;
    public bool GizmosDrawCubes = true;
    public bool ShowDebug = true;
    public bool ShowBounds = true;
    public bool MoveEmptyToBounds = false;
    [Header("Random Spawn Settings")]
    [Range(5,25)]public int RandomSpawnRange = 5;

    public void Step()
    {
        if (data.Cubes.Count == 0) 
        {
            SpawnRandom();
        }
        else
        {
            data.ApplyRulesToSpawnCubesInto(GameOfLifeSurviveValue, MaxCubes);
            OnDictionaryChange();
        }
    }
    public void ClearDictionary()
    {
        data.ClearCubesDictionary();
        OnDictionaryChange();
    }
    public void SpawnRandom()
    {
       int count = Random.Range(30, 75);
        
        for (int i = 0; i < count; i++)
        {
            int size = RandomSpawnRange + (int)(data.Cubes.Count * 0.05f);
            Vector3 randomVector = new Vector3(Random.Range(-size, size), Random.Range(-size, size), Random.Range(-size, size));
            data.SetCube(randomVector);
        }
        OnDictionaryChange();
    }
    public void OnDictionaryChange()
    {
        data.CalculateActiveCubes();
        if(ShowBounds || MoveEmptyToBounds) data.CalculateBounds();
        if(MoveEmptyToBounds) MoveEmptyToCenterOfBounds();
        
    }
    private void MoveEmptyToCenterOfBounds()
    {
        transform.position = data.Bounds[2];
    }
    private void OnDrawGizmos()
    {
        if (data == null) return;
        if (data.Cubes.Count > 0)
        {
            #region Render Cubes
            float lerp = ((data.NumberOfCubes / (float)MaxCubes)-0.5f)*2;
            Gizmos.color = Color.Lerp(StartingColor, EndingColor, Mathf.Clamp(lerp, 0,1)) ;

            if(GizmosDrawCubes) foreach (Vector3 cub in data.Cubes) Gizmos.DrawCube(cub, Vector3.one);
            Gizmos.color = Color.white;
            if(ShowDebug) foreach (var keyPair in data.ActiveCubes) DrawNumber(keyPair.Key, keyPair.Value);

            #endregion
            #region Render Bounds
            Gizmos.color = Color.green;
            if (ShowBounds) Gizmos.DrawWireCube(data.Bounds[2], data.Bounds[3]+(Vector3.one));//0 Min, 1 Max, 2 Average, 3 Range
            #endregion
        }
    }
    private void DrawNumber(Vector3 pos, int num)
    {
        Gizmos.color = Color.white;
        Gizmos.DrawIcon(pos, num.ToString() + ".png", true);
    }


}
