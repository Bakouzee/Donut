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

        private bool _canOnShot;
        private bool _isInvincible;
        private bool _canResetHeath;
        private bool _isGodMod;
        
        private int _lastID;

        private void OnEnable()
        {
            source = (CheatManager)target;

            RefreshToggle();
        }

        private void RefreshToggle()
        {
            if (!source.IsInitialized) return;
            
            _canOnShot = source.FindReceiver().Exists(x => x.Fighter.CanOneShot);
            _isInvincible = source.FindReceiver().Exists(x => x.Fighter.IsInvincible);
            _canResetHeath = source.FindReceiver().Exists(x => x.Fighter.IsFullHealth);
            _isGodMod = source.FindReceiver().Exists(x => x.Fighter.CanOneShot && x.Fighter.IsInvincible && x.Fighter.IsFullHealth);
            _lastID = (int)source.cheatReceiverState;
        }
        private void ApplyAction(System.Action act)
        {
            act.Invoke();
            RefreshToggle();
        }
        public override void OnInspectorGUI()
        {
            source = (CheatManager)target;

            if (!Application.isPlaying || !source.IsInBattle)
            {
                EditorGUILayout.HelpBox("Enable on runtime and in the Battle only", MessageType.Warning);
                return;
            }

            source.cheatReceiverState = (CheatManager.CheatReceiver)EditorGUILayout.EnumPopup(new GUIContent("Cheat ID", "cheatReceiverState"), source.cheatReceiverState);

            if (_lastID != (int)source.cheatReceiverState)
                RefreshToggle();

            GUILayout.BeginHorizontal("box");

            GUI.color = _isGodMod ? Color.green : Color.red;
            if (GUILayout.Button("GodMode"))
            {
                ApplyAction(() => source.AddSetGodMode());
            }
            
            GUI.color = _isInvincible ? Color.green : Color.red;
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
            
            GUI.color = _canResetHeath ? Color.green : Color.red;
            if (GUILayout.Button("ResetHealth"))
            {
                ApplyAction(() => source.AddSetFullHealth());
            }

            GUILayout.EndHorizontal();
            
        }
    }
}
