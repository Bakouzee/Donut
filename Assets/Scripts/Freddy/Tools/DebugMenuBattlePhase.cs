/*using Com.Donut.BattleSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
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
        if (enemies.Count > 0)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Name = EditorGUILayout.TextField("Name", enemies[i].Name);
                enemies[i].Level = EditorGUILayout.TextField("Level", enemies[i].Level);
                enemies[i].Sprite = EditorGUILayout.ObjectField("Sprite", enemies[i].Sprite, typeof(Sprite), true) as Sprite;
                enemies[i].AnimatorController = EditorGUILayout.ObjectField("Animator Controller", enemies[i].AnimatorController, typeof(AnimatorController), true) as AnimatorController;
                enemies[i].TotalHealth = EditorGUILayout.IntField("Total Health", enemies[i].TotalHealth);
                enemies[i].CurrentHealth = EditorGUILayout.IntField("Current Health", enemies[i].CurrentHealth);
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

        GUILayout.BeginHorizontal();
        bool add = GUILayout.Button(new GUIContent("ADD"));
        bool save = GUILayout.Button(new GUIContent("SAVE"));
        bool delete = GUILayout.Button(new GUIContent("DELETE"));
        GUILayout.EndHorizontal();

        isShown = EditorGUILayout.Foldout(isShown, "Enemies");

        if (isShown)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (isEnemyStatsShown.Count > 0)
                {
                    if (enemies[i].Name == "")
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
                    enemies[i].Power = EditorGUILayout.IntField("Power", enemies[i].Power);
                    enemies[i].Healing = EditorGUILayout.IntField("Healing", enemies[i].Healing);
                    enemiesToDelete.Add(false);
                    enemiesToDelete[i] = EditorGUILayout.Toggle("Delete ?", enemiesToDelete[i]);
                    GUILayout.Space(30);
                }
            }
        }

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
                        Debug.Log("Enemy successfully added !");
                    }
                    else
                    {
                        int j = 0;
                        foreach (string GUIDName in AssetDatabase.FindAssets(enemies[i].Name, new[] { "Assets/Enemies" }))
                        {
                            string pathName = AssetDatabase.GUIDToAssetPath(GUIDName);
                            string[] tabName = pathName.Split('/');
                            string[] realName = tabName[2].Split('.');

                            foreach (Fighter enemy in enemies)
                            {
                                if (enemy.Name == realName[0])
                                {
                                    j++;
                                    if (j > 1)
                                    {
                                        throw new Exception("To rename an enemy, you must write a different Name to an already existing enemy !");
                                    }
                                }
                            }
                        }
                        AssetDatabase.SaveAssets();
                        string nameToSave = enemies[i].Name;

                        AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(enemies[i].GetInstanceID()), enemies[i].Name);

                        enemies[i].Name = nameToSave;
                        Debug.Log("Enemy successfully renamed !");
                    }
                }
                catch
                {
                    throw new Exception("To create a new enemy, you must write a Name to your new enemy !");
                }
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
*/