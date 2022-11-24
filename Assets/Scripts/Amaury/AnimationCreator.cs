using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

public class AnimationCreator : MonoBehaviour {

    [System.Serializable]
    public class MotionParams {
        public Motion motion;
        public AnimatorTransition transition;
    }
    
    public string animName;
    public string animPath;
    public Texture2D texture;

    public RuntimeAnimatorController animator;
    public bool isLooping;
    public int timeBetweenSprite;

    public GameObject target;

    public string animControllerName,animControllerPath;

    public List<Motion> motions = new List<Motion>();
    public List<MotionParams> m_params;

}
