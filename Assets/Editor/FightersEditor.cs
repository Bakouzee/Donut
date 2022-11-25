using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Com.Donut.BattleSystem
{
    [CustomEditor(typeof(Fighters))]
    public class FightersEditor : Editor
    {
        private Fighters myObject = null;
        private string infos = "";

        private void OnEnable()
        {
            this.myObject = (Fighters)this.target;
        }

        public override void OnInspectorGUI()
        {
            List<int> enemyNumber = new List<int>();

            for (int i = 0; i < myObject.Database.EnemiesList.Count; i++)
            {
                Rect r = EditorGUILayout.BeginVertical();
                float diff = (float)myObject.Database.EnemiesList[i].Power / 1000;
                EditorGUI.ProgressBar(r, diff, myObject.Database.EnemiesList[i].Name);
                GUILayout.Space(20);
                EditorGUILayout.EndVertical();
                GUILayout.Space(2);
            }

            base.OnInspectorGUI();

            if (myObject.EnemiesToAdd.Count <= 0)
            {
                infos = "Add at least an enemy!";
            }

            if (GUILayout.Button("Add to the database"))
            {
                if (myObject.EnemiesToAdd.Count > 0)
                {
                    infos = "Enemies successfully added!";
                }
                for (int i = 0; i < myObject.EnemiesToAdd.Count; i++)
                {
                    if (myObject.EnemiesToAdd[i].Name != "")
                    {
                        myObject.Database.EnemiesList.Add(myObject.EnemiesToAdd[i]);
                    }
                    else
                    {
                        enemyNumber.Add(i);
                    }
                }

                for(int i = 0; i < enemyNumber.Count; i++)
                {
                    if(i > 0 && i < enemyNumber.Count - 1)
                    {
                        infos += ", " + enemyNumber[i].ToString();
                    }
                    else if(i == 0)
                    {
                        infos = "Those enemies ";
                        infos += enemyNumber[i].ToString();
                        if (i == enemyNumber.Count - 1)
                        {
                            infos += " must have a name!";
                        }
                    } else if(i == enemyNumber.Count - 1)
                    {
                        infos += " and " + enemyNumber[i].ToString() + " must have a name!";
                    }
                }
            }

            //string EditorGUILayout.TextField()

            EditorGUILayout.Space(20);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.HelpBox(infos, MessageType.Info);
            EditorGUILayout.EndHorizontal();
        }
    }
}