using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConwayCubesRenderer : MonoBehaviour
{
    private ConwayCubes3DGrid conwayCubes3DGrid;
    private void OnValidate()
    {
        if (conwayCubes3DGrid == null) conwayCubes3DGrid = GetComponent<ConwayCubes3DGrid>();
    }
    [Header("Settings")]
    public Color StartingColor = Color.green;
    public Color EndingColor = Color.blue;
    public Color OnColor= Color.white;
    public Color OffColor = Color.black;
    public void RenderCubes()
    {
        
    }
    public void OnDrawGizmos()
    {
        List<int> enabledCubes = new List<int>(0);
        Gizmos.color = OffColor;
        for (int i = 0; i < conwayCubes3DGrid.Cubes.Length; i++)
        {
            //Gizmos.color = Color.Lerp(StartingColor, EndingColor, ((float)i / (float)conwayCubes3DGrid.Cubes.Length));
            if(conwayCubes3DGrid.GetCubeState(i) == false)
            {
                Gizmos.DrawCube(conwayCubes3DGrid.GetCubePos(i), Vector3.one);
            }
            else
            {
                enabledCubes.Add(i);
            }
        }
        Vector3 pos = Vector3.zero;
        Vector3 oldpos = Vector3.zero;
        for (int i = 0; i < conwayCubes3DGrid.Cubes.Length; i++)
        {
            Gizmos.color = Color.Lerp(StartingColor, EndingColor, ((float)i / (float)conwayCubes3DGrid.Cubes.Length));
            pos = conwayCubes3DGrid.GetCubePos(i);
            if (pos != Vector3.zero) Gizmos.DrawLine(pos, oldpos);
            oldpos = pos;
        }
        Gizmos.color = OnColor;
        foreach (var cub in enabledCubes)
        {
            Gizmos.DrawCube(conwayCubes3DGrid.GetCubePos(cub), Vector3.one);
        }
    }
}
