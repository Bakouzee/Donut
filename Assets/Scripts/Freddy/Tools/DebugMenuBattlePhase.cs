using Com.Donut.BattleSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DebugMenuBattlePhase))]
public class DebugMenuBattlePhase : EditorWindow
{ 
    private static int numberEnemiesCreated = 0;

    [MenuItem("Tools/Debug Menu/Battle Phase")]
    static void InitDebugMenu()
    {
        DebugMenuBattlePhase window = GetWindow<DebugMenuBattlePhase>();
        window.titleContent = new GUIContent("Debug Menu : Battle Phase");
        window.Show();
       // this.myObject = (DebugMenuBattlePhase)this.target;
    }

    private void OnGUI()
    { 
        bool save = GUILayout.Button(new GUIContent("SAVE"));
        bool add = GUILayout.Button(new GUIContent("ADD"));

        if (add)
        {
            AllFighters newEnemy = CreateInstance<AllFighters>();
            newFighter.Name = GUILayout.TextField(newFighter.Name);
            newEnemy.fighters.Add() = 
            AssetDatabase.CreateAsset(newEnemy, "Assets/Enemies/Enemy" + ++numberEnemiesCreated + ".asset");
        }

        if (save)
        {
            AssetDatabase.SaveAssets();
        }
    }
}
