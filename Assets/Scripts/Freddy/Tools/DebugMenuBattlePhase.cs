using Com.Donut.BattleSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

//[CustomEditor(typeof(DebugMenuBattlePhase))]
public class DebugMenuBattlePhase : EditorWindow
{ 
    private static int numberEnemiesCreated = 0;
    private static List<AllFighters> enemies = new List<AllFighters>();
    private static List<bool> enemiesToDelete = new List<bool>();
    private Vector2 scrollPos = Vector2.zero;

    [MenuItem("Tools/Debug Menu/Battle Phase")]
    static void InitDebugMenu()
    {
        DebugMenuBattlePhase window = GetWindow<DebugMenuBattlePhase>();
        window.titleContent = new GUIContent("Debug Menu : Battle Phase");
        window.Show();
        if(enemies.Count > 0)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Name = EditorGUILayout.TextField("Name", enemies[i].Name);
                enemies[i].Level = EditorGUILayout.TextField("Level", enemies[i].Level);
                enemies[i].Sprite = EditorGUILayout.ObjectField("Sprite", enemies[i].Sprite, typeof(Sprite), true) as Sprite;
                enemies[i].AnimatorController = EditorGUILayout.ObjectField("Animator Controller", enemies[i].AnimatorController, typeof(AnimatorController), true) as AnimatorController;
                enemies[i].TotalHealth = EditorGUILayout.IntField("Total Health", enemies[i].TotalHealth);
                enemies[i].CurrentHealth = EditorGUILayout.IntField("Current Health", enemies[i].CurrentHealth);
                enemies[i].MinDamage = EditorGUILayout.IntField("Min Damage", enemies[i].MinDamage);
                enemies[i].MaxDamage = EditorGUILayout.IntField("Max Damage", enemies[i].MaxDamage);
                enemies[i].Power = EditorGUILayout.IntField("Power", enemies[i].Power);
                enemies[i].Healing = EditorGUILayout.IntField("Healing", enemies[i].Healing);
                enemiesToDelete.Add(false);
                enemiesToDelete[i] = EditorGUILayout.Toggle("Delete ?", enemiesToDelete[i]);
            }
        }
    }

    private void OnGUI()
    {
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

        bool add = GUILayout.Button(new GUIContent("ADD")); 

        if (add)
        {
            enemies.Add(new AllFighters());
        }

        for(int i = 0; i < enemies.Count; i++)
        {
            enemies[i].Name = EditorGUILayout.TextField("Name", enemies[i].Name);
            enemies[i].Level = EditorGUILayout.TextField("Level", enemies[i].Level);
            enemies[i].Sprite = EditorGUILayout.ObjectField("Sprite", enemies[i].Sprite, typeof(Sprite), true) as Sprite;
            enemies[i].AnimatorController = EditorGUILayout.ObjectField("Animator Controller", enemies[i].AnimatorController, typeof(AnimatorController), true) as AnimatorController;
            enemies[i].TotalHealth = EditorGUILayout.IntField("Total Health", enemies[i].TotalHealth);
            enemies[i].CurrentHealth = EditorGUILayout.IntField("Current Health", enemies[i].CurrentHealth);
            enemies[i].MinDamage = EditorGUILayout.IntField("Min Damage", enemies[i].MinDamage);
            enemies[i].MaxDamage = EditorGUILayout.IntField("Max Damage", enemies[i].MaxDamage);
            enemies[i].Power = EditorGUILayout.IntField("Power", enemies[i].Power);
            enemies[i].Healing = EditorGUILayout.IntField("Healing", enemies[i].Healing);
            enemiesToDelete.Add(false);
            enemiesToDelete[i] = EditorGUILayout.Toggle("Delete ?", enemiesToDelete[i]);
        }

        bool save = GUILayout.Button(new GUIContent("SAVE"));
        bool clear = GUILayout.Button(new GUIContent("CLEAR"));

        if (save)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                AssetDatabase.CreateAsset(enemies[i], "Assets/Enemies/Enemy" + ++numberEnemiesCreated + ".asset");
            }
            AssetDatabase.SaveAssets();
        }

        if (clear)
        {
            for (int i = 0; i < enemiesToDelete.Count; i++)
            {
                if (enemiesToDelete[i] == true)
                {
                    AssetDatabase.DeleteAsset("Assets/Enemies/Enemy" + (i + 1) + ".asset");
                    enemiesToDelete.RemoveAt(i);
                    enemies.RemoveAt(i);
                }
            }
        }
        EditorGUILayout.EndScrollView();
    }
}
