using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(ConwayCubes3DGrid))]
public class ConwayCubes3DGridEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        ConwayCubes3DGrid grid = (ConwayCubes3DGrid)target; // Get reference to the target object
        //if (GUILayout.Button("Reset Grid"))
        //{
        //    grid.ResetGrid();
        //}
    }
}
