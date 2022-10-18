using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ChangeSpriteColor))]
public class ChangeSpriteColorEditor : Editor
{
    /*private ChangeSpriteColor myObject = null;

    private void OnEnable()
    {
        this.myObject = (ChangeSpriteColor)this.target;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("My Label");
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.HelpBox("Error maxime en retard", MessageType.Error);
        EditorGUILayout.Space(300f, true);

        this.myObject.myText = EditorGUILayout.TextField("Mt Text", this.myObject.myText);
    }*/
}
