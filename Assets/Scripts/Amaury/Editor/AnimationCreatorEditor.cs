using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Rendering;

[CustomEditor(typeof(AnimationCreator))]
public class AnimationCreatorEditor : Editor {

    private AnimationCreator animCreator;
    private SerializedObject animClass;

    private SerializedProperty animName,animPath,texture,isLooping,timeBetweenSprite,targetPlayer;

    private void OnEnable() {
        animCreator = (AnimationCreator)this.target;
        animClass = new SerializedObject(animCreator);

        animName = animClass.FindProperty("animName");
        animPath = animClass.FindProperty("animPath");
        texture = animClass.FindProperty("texture");
        isLooping = animClass.FindProperty("isLooping");
        timeBetweenSprite = animClass.FindProperty("timeBetweenSprite");
        targetPlayer = animClass.FindProperty("target");
    }

    public override void OnInspectorGUI() {
        EditorGUILayout.LabelField("Texture Params", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(texture);
        EditorGUILayout.Space(30);
        
        EditorGUILayout.LabelField("File Params", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(animName);
        EditorGUILayout.PropertyField(animPath);
        EditorGUILayout.PropertyField(targetPlayer);
        EditorGUILayout.Space(30);
        
        EditorGUILayout.LabelField("Anim Params", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(isLooping);
        EditorGUILayout.PropertyField(timeBetweenSprite);
        EditorGUILayout.Space();
        

  //      animCreator.animator.clip
        
        if (GUILayout.Button("Generate Animations")) {
            AnimationClip clip = new AnimationClip();

            
            
            Sprite[] sprites = Resources.LoadAll<Sprite>(animCreator.texture.name);
            ObjectReferenceKeyframe[] objFrames = new ObjectReferenceKeyframe[sprites.Length];
            
            Debug.Log("size " + sprites.Length);

            for (int i = 0; i < sprites.Length; i++) {
                objFrames[i].time = animCreator.timeBetweenSprite / 60f * i;
                objFrames[i].value = sprites[i];
            }

            if (animCreator.isLooping) { // Not working
                clip.wrapMode = WrapMode.Loop;
            }

            Debug.Log("enter");
            animCreator.target.GetComponent<SpriteRenderer>().sprite.GetType();

            AnimationUtility.SetObjectReferenceCurve(clip,EditorCurveBinding.DiscreteCurve("",null,"Sprite"), objFrames);
            AssetDatabase.CreateAsset(clip,"Assets/" + animCreator.animPath + "/" + animCreator.animName + ".anim");
        }
        
        if(EditorGUI.EndChangeCheck()) 
            animClass.ApplyModifiedProperties();
    }
}
