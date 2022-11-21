using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Com.Donut.BattleSystem
{
    [CustomEditor(typeof(CheatManager))]
    public class CheatManagerEditor : Editor
    {
        CheatManager source;

        public bool godModeBool;

        private bool _canOnShot;
        private int _lastID;

        private void OnEnable()
        {
            source = (CheatManager)target;

            RefreshToggle();
        }

        private void RefreshToggle()
        {
            _canOnShot = source.FindReceiver().Exists(x => x.Fighter.CanOneShot);
            _lastID = (int)source.cheatReceiverState;
        }
        private void ApplyAction(System.Action act)
        {
            act.Invoke();
            //source.ApplyCheats();
            RefreshToggle();
        }
        public override void OnInspectorGUI()
        {
            //DrawDefaultInspector();

            source = (CheatManager)target;

            if (!Application.isPlaying)
            {
                EditorGUILayout.HelpBox("Enable on runtime only", MessageType.Warning);
                return;
            }

            source.cheatReceiverState = (CheatManager.CheatReceiver)EditorGUILayout.EnumPopup(new GUIContent("Cheat ID", "cheatReceiverState"), source.cheatReceiverState);

            if (_lastID != (int)source.cheatReceiverState)
                RefreshToggle();

            GUILayout.BeginHorizontal("box");

            if (GUILayout.Button("GodMode"))
            {
                source.AddSetGodMode();
            }
            //godModeBool = GUI.Toggle(new Rect(10, 10, 100, 30), godModeBool , "A Toggle text");

            if (GUILayout.Button("Invincibility"))
            {
                ApplyAction(() => source.AddSetInvincible());
            }

            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal("box");

            GUI.color = _canOnShot ? Color.green : Color.red;
            if (GUILayout.Button("CanOneShot"))
            {
                ApplyAction(() => source.AddCanOneShot());
            }
            GUI.color = Color.white;

            if (GUILayout.Button("ResetHealth"))
            {
                ApplyAction(() => source.AddResetHealth());
            }

            GUILayout.EndHorizontal();

            if (GUILayout.Button("Apply Cheats"))
            {
                source.ApplyCheats();
            }
        }
    }
}
