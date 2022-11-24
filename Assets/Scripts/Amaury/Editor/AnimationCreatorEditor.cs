using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using UnityEditor.Rendering;




[CustomEditor(typeof(AnimationCreator))]
public class AnimationCreatorEditor : Editor {

    private AnimationCreator animCreator;
    private SerializedObject animClass;

    private SerializedProperty animName,animPath,texture,isLooping,timeBetweenSprite,targetPlayer;
    private SerializedProperty animControllerName, animControllerPath;
    private SerializedProperty motions,m_params;

    private void OnEnable() {
        animCreator = (AnimationCreator)this.target;
        animClass = new SerializedObject(animCreator);

        animName = animClass.FindProperty("animName");
        animPath = animClass.FindProperty("animPath");
        texture = animClass.FindProperty("texture");
        isLooping = animClass.FindProperty("isLooping");
        timeBetweenSprite = animClass.FindProperty("timeBetweenSprite");
        targetPlayer = animClass.FindProperty("target");
        animControllerName = animClass.FindProperty("animControllerName");
        animControllerPath = animClass.FindProperty("animControllerPath");
        motions = animClass.FindProperty("motions");
        m_params = animClass.FindProperty("m_params");
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
        
        
        EditorGUILayout.Space(10);
        
        if (GUILayout.Button("Generate Animations")) {
            AnimationClip clip = new AnimationClip();

            Sprite[] sprites = Resources.LoadAll<Sprite>(animCreator.texture.name);
            ObjectReferenceKeyframe[] objFrames = new ObjectReferenceKeyframe[sprites.Length];
           
            Debug.Log("length " + sprites.Length);
            
            for (int i = 0; i < sprites.Length; i++) {
                objFrames[i].time = animCreator.timeBetweenSprite / 60f * i;
                objFrames[i].value = sprites[i];
            }

            if (animCreator.isLooping) { // Not working
                clip.wrapMode = WrapMode.Loop;
            }

            AnimationUtility.SetObjectReferenceCurve(clip,EditorCurveBinding.DiscreteCurve("",typeof(SpriteRenderer),"m_Sprite"), objFrames);
            AssetDatabase.CreateAsset(clip,"Assets/" + animCreator.animPath + "/" + animCreator.animName + ".anim");
        }
        
        
        EditorGUILayout.Space(30);
        
        EditorGUILayout.LabelField("Animator Params",EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(animControllerPath);
        EditorGUILayout.PropertyField(animControllerName);
        
        
        EditorGUILayout.Space(10);
        
        if (GUILayout.Button("Generate Animator")) 
            AnimatorController.CreateAnimatorControllerAtPath("Assets/" + animCreator.animControllerPath + "/" + animCreator.animControllerName + ".controller");

        EditorGUILayout.Space(30);
        
        EditorGUILayout.LabelField("Motions Params", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(motions);
        
        EditorGUILayout.Space(10);
        
        if (GUILayout.Button("Add Motions")) {
            AnimatorController controller = (AnimatorController) AssetDatabase.LoadAssetAtPath("Assets/" + animCreator.animControllerPath + "/" + animCreator.animControllerName + ".controller",typeof(AnimatorController));

            foreach (Motion anim in animCreator.motions) 
                controller.AddMotion(anim);
            
            Debug.Log("layer " + controller.layers[0] + " size " + controller.layers.Length);

            foreach (Motion motion in animCreator.motions)
            {
                List<AnimationCreator.MotionParams> currentParams = animCreator.m_params.Where(m_params => m_params.motion == motion).ToList(); // A TESTER VOIR SI NUL

                List<AnimatorTransition> currentTransitions = new List<AnimatorTransition>();

                foreach (AnimationCreator.MotionParams m_params in currentParams)
                    currentTransitions.Add(m_params.transition);

                controller.layers[0].stateMachine.SetStateMachineTransitions(controller.layers[0].stateMachine,currentTransitions.ToArray());   
            }
        }
        
        
        EditorGUILayout.PropertyField(m_params);
        
        if(EditorGUI.EndChangeCheck()) 
            animClass.ApplyModifiedProperties();
    }
}
