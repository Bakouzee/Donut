using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Com.Donut.BattleSystem
{
    [CustomEditor(typeof(CheatManager))]
    public class CheatManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            CheatManager manager = (CheatManager)target;

            GUIStyle style = new GUIStyle()
            {
                alignment = TextAnchor.MiddleCenter,
                fontStyle = FontStyle.Bold,

                normal = new GUIStyleState()
                {
                    background = Texture2D.whiteTexture
                },

                active = new GUIStyleState()
                {
                    background = Texture2D.blackTexture
                }
            };
            GUILayout.BeginHorizontal("box");

            if (GUILayout.Button("GodMode"))
            {
                manager.AddSetGodMode();
            }

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
