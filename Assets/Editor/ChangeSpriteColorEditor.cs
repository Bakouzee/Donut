using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(ChangeSpriteColor))]
public class ChangeSpriteColorEditor : Editor
{
    private ChangeSpriteColor myObject = null;


    private void OnEnable()
    {
        this.myObject = (ChangeSpriteColor)this.target;
 
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("My label");
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.HelpBox("coucou", MessageType.Info);
        EditorGUILayout.Space(300,true);

        this.myObject.myText = EditorGUILayout.TextField("My Text", this.myObject.myText);
    }
}
