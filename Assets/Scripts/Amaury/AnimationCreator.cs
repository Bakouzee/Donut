using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationCreator : MonoBehaviour
{

    public string animName;
    public string animPath;
    public Texture2D texture;

    public RuntimeAnimatorController animator;
    public bool isLooping;
    public int timeBetweenSprite;

    public GameObject target;

}
