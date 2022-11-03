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
    //private static int numberEnemiesCreated = 0;
    private static List<Fighter> enemies = new List<Fighter>();
    private static List<bool> enemiesToDelete = new List<bool>();
    private Vector2 scrollPos = Vector2.zero;
    private static bool isShown;
    private static List<bool> isEnemyStatsShown = new List<bool>();

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
                isEnemyStatsShown.Add(new bool());
            }
        }
    }

    private void OnGUI()
    {
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
        
        isShown = EditorGUILayout.Foldout(isShown, "Enemies");

        if (isShown)
        {
            for(int i = 0; i < enemies.Count; i++)
            {
                if(isEnemyStatsShown.Count > 0)
                {
                    if(enemies[i].Name == "")
                    {
                        isEnemyStatsShown[i] = EditorGUILayout.Foldout(isEnemyStatsShown[i], "New Enemy");
                    }
                    else
                    {
                        isEnemyStatsShown[i] = EditorGUILayout.Foldout(isEnemyStatsShown[i], enemies[i].Name);
                    }
                }

                if (isEnemyStatsShown[i])
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
                    GUILayout.Space(30);
                }
            }
        }

        GUILayout.BeginHorizontal();
        bool add = GUILayout.Button(new GUIContent("ADD")); 
        bool save = GUILayout.Button(new GUIContent("SAVE"));
        bool delete = GUILayout.Button(new GUIContent("DELETE"));
        GUILayout.EndHorizontal();

        if (add)
        {
            enemies.Add(new Fighter());
            isEnemyStatsShown.Add(false);
        }

        if (save)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                try
                {
                    if(!AssetDatabase.Contains(enemies[i].GetInstanceID()))
                    {
                        AssetDatabase.CreateAsset(enemies[i], "Assets/Enemies/" + enemies[i].Name + ".asset");
                        AssetDatabase.SaveAssets();
                    }
                    else
                    {
                        AssetDatabase.SaveAssets();
                        //AssetDatabase.RenameAsset("Assets/Enemies/" + enemies[i].name + ".asset", enemies[i].Name + ".asset");
                        AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(enemies[i].GetInstanceID()), enemies[i].Name);
                    }
                }
                catch
                {
                    throw new Exception("To create a new enemy, you must write a Name to your new enemy !");
                }
                Debug.Log("Enemy successfully added !");
            }
        }

        if (delete)
        {
            for (int i = 0; i < enemiesToDelete.Count; i++)
            {
                if (enemiesToDelete[i] == true)
                {
                    AssetDatabase.DeleteAsset("Assets/Enemies/" + enemies[i].Name + ".asset");
                    enemiesToDelete.RemoveAt(i);
                    enemies.RemoveAt(i);
                }
            }
        }
        EditorGUILayout.EndScrollView();
    }
}
