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

    [Header("Settings Renderer")]
    public Color EnabledColor = Color.white;
    public Color EnabledTextColor = Color.white;
    public Color DisbledColor = Color.white;
    public Color DisbledTextColor = Color.white;

    public void Step()
    {
        OnDictionaryChange();
    }
    public void ClearDictionary()
    {
        data.ClearCubesDictionary();
        OnDictionaryChange();
    }
    public void SpawnRandom()
    {
       int count = Random.Range(1, 10);
        int size = 5 + (int)(data.Cubes.Count * 0.1f);
        for (int i = 0; i < count; i++)
        {
            data.SetCube(new Vector3(Random.Range(-size, size), Random.Range(-size, size), Random.Range(-size, size)), true);
        }
        OnDictionaryChange();
    }
    public void OnDictionaryChange()
    {
        data.CalcEnabledDisabledCubes();
        data.CalculateBounds();
    }
    private void OnDrawGizmos()
    {
        if (data == null) return;
        if (data.Cubes.Count > 0)
        {
            #region Render Cubes
            foreach (Vector3 cub in data.EnabledCubes)
            {
                Gizmos.color = EnabledColor;
                Gizmos.DrawCube(cub, Vector3.one);
                Gizmos.color = EnabledTextColor;
                DrawNumber(cub, 1);
            }
            foreach (Vector3 cub in data.DisabledCubes)
            {
                Gizmos.color = DisbledColor;
                Gizmos.DrawCube(cub, Vector3.one);
                Gizmos.color = DisbledTextColor;
                DrawNumber(cub, 1);
            }

            #endregion
            #region Render Bounds
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(data.Bounds[2], data.Bounds[3]+(Vector3.one));//0 Min, 1 Max, 2 Average, 3 Range
            #endregion
        }





    }
    private void DrawNumber(Vector3 pos, int num)
    {
        Gizmos.color = Color.white;
        Gizmos.DrawIcon(pos, num.ToString() + ".png", true);
    }


}
