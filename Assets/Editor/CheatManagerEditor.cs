using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Com.Donut.BattleSystem
{
    [CustomEditor(typeof(CheatManager))]
    public class CheatManagerEditor : Editor
    {
        public bool godModeBool;
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            CheatManager manager = (CheatManager)target;

            

            GUILayout.BeginHorizontal("box");

            if (GUILayout.Button("GodMode"))
            {
                manager.AddSetGodMode();
            }
            //godModeBool = GUI.Toggle(new Rect(10, 10, 100, 30), godModeBool , "A Toggle text");

            if (GUILayout.Button("Invincibility"))
            {
                manager.AddSetInvincible();
            }

            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal("box");

            if (GUILayout.Button("CanOneShot"))
            {
                manager.AddCanOneShot();
            }
            
            if (GUILayout.Button("ResetHealth"))
            {
                manager.AddResetHealth();
            }

            GUILayout.EndHorizontal();

            if (GUILayout.Button("Apply Cheats"))
            {
                manager.ApplyCheats();
            }
        }
    }
}
