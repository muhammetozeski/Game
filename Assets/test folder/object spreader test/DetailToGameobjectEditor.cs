using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DetailToGameObject))]
public class DetailToGameobjectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        DetailToGameObject script = (DetailToGameObject)target;

        GUIContent GameObjects = new GUIContent("Game Objects");
        script.ChoosenGameObjectIndex = EditorGUILayout.Popup(GameObjects, script.ChoosenGameObjectIndex, script.GameObjects);
        
        script.assignGameObject(script.ChoosenGameObjectIndex);

        if (GUILayout.Button("Detect Game Objects"))
        {
            script.detectGameObjects();
        }

        if(GUILayout.Button("Convert Details To Game Objects"))
        {
            script.ConvertDetailsToGameObjects();
        }

        GUILayout.Space(10);

        if(GUILayout.Button("Convert \"ALL\" Details To Game Objects"))
        {
            script.ConvertAllDetailsToGameObjects();
        }
    }
}
