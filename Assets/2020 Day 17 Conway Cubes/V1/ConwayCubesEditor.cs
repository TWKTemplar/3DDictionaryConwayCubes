using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(ConwayCubes))]
public class ConwayCubesEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        ConwayCubes conwayCubes = (ConwayCubes)target; // Get reference to the target object
        if (GUILayout.Button("Step"))
        {
            conwayCubes.Step();
        }
        if (GUILayout.Button("Clear"))
        {
            conwayCubes.Clear();
        }
    }
}
