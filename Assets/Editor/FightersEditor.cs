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

            EditorGUILayout.BeginHorizontal();
            EditorGUI.ProgressBar(new Rect(new Vector2(15, 15), new Vector2(250, 50)), 2, "Difficulty");
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(70);

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
                    } else if(i == enemyNumber.Count - 1)
                    {
                        infos += " and " + enemyNumber[i].ToString() + " must have a name!";
                    }
                }
            }

            EditorGUILayout.Space(20);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.HelpBox(infos, MessageType.Info);
            EditorGUILayout.EndHorizontal();
        }
    }
}