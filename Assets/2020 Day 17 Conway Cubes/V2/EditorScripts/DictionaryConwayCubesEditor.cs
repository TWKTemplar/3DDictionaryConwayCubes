using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(DictionaryConwayCubes))]
public class DictionaryConwayCubesEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        DictionaryConwayCubes DconwayCubes = (DictionaryConwayCubes)target; // Get reference to the target object
        if (GUILayout.Button("Clear Dictionary"))
        {
            DconwayCubes.ClearDictionary();
        }
        if (GUILayout.Button("Spawn Random In Dictionary"))
        {
            DconwayCubes.SpawnRandom();
        }
        if (GUILayout.Button("Step"))
        {
            DconwayCubes.Step();
        }
    }
}
