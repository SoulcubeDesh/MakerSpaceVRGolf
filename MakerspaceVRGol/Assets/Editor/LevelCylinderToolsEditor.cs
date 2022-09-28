using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelCylinderTools))]
public class LevelCylinderToolsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        LevelCylinderTools myScript = (LevelCylinderTools)target;
        if (GUILayout.Button("Generate Cylinders"))
        {
            myScript.GenerateCylinders();
        }
    }
}
