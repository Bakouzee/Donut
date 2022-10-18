using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(DebugMenuBattlePhase))]
public class DebugMenuBattlePhase : EditorWindow
{
    private DebugMenuBattlePhase myObject = null; 

    [SerializeField] private struct Enemy
    {
        public Sprite spr;
        public string name;
    }

    [SerializeField] private List<Enemy> enemies = new List<Enemy>();

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
        bool add = GUILayout.Button(new GUIContent("ADD"));

        if (add)
        {
            GUILayout.BeginHorizontal();
           // EditorGUILayout.ObjectField()
            GUILayout.EndHorizontal();
        }
    }
}
