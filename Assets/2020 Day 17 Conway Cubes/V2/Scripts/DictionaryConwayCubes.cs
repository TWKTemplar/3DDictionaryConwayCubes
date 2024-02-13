using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictionaryConwayCubes : MonoBehaviour
{
  


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
    public Color NonFertileDebugImageColor = Color.white;
    [Header("Settings")]    
    public int GameOfLifeSurviveValue = 4;
    public int MaxCubes = 50000;
    public bool GizmosDrawCubes = true;
    public bool ShowDebug = true;
    public bool ShowBounds = true;
    [Header("Random Spawn Settings")]
    [Range(5,25)]public int RandomSpawnRange = 5;

    public void Step()
    {
        if (data.Cubes.Count == 0) SpawnRandom();
        else data.ApplyRulesToCubes(GameOfLifeSurviveValue, MaxCubes);
        OnDictionaryChange();
    }
    public void ClearDictionary()
    {
        data.ClearCubes();
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
        data.CalculateFertilityMap();
        if(ShowBounds) data.CalculateBounds();
    }
    private void OnDrawGizmos()
    {
        if (data.Cubes.Count > 0)
        {
            float lerpColor = ((data.NumberOfCubes / (float)MaxCubes)-0.5f)*2;
            Gizmos.color = Color.Lerp(StartingColor, EndingColor, Mathf.Clamp(lerpColor, 0,1)) ;

            if(GizmosDrawCubes) foreach (Vector3 cub in data.Cubes) Gizmos.DrawCube(cub, Vector3.one);
            Gizmos.color = Color.white;
            if(ShowDebug) foreach (var keyPair in data.FertilityMap) DrawNumber(keyPair.Key, keyPair.Value);

            Gizmos.color = Color.green;
            if (ShowBounds) RenderBounds();
        }
    }
    private void RenderBounds()
    {
        Gizmos.DrawWireCube(data.Bounds[2], data.Bounds[3] + (Vector3.one));//0 Min, 1 Max, 2 Average, 3 Range
    }
    private void DrawNumber(Vector3 pos, int num)
    {
        Gizmos.color = Color.white;
       // if(GameOfLifeSurviveValue == num) Gizmos.color = NonFertileDebugImageColor;
        Gizmos.DrawIcon(pos, num.ToString() + ".png", true);
    }
}
