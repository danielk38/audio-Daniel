using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CubeManager))]
public class CubeManagerEditor : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var script = (CubeManager)target;
        if (GUILayout.Button("Create circle"))
            script.CreateCircle(256);
    }
}
