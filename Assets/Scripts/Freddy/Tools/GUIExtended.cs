using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityToolbarExtender;

[InitializeOnLoad]
public class GUIExtended
{
    static GUIExtended()
    {
        ToolbarExtender.LeftToolbarGUI.Add(OnLeftToolbarGUI);
        ToolbarExtender.RightToolbarGUI.Add(OnRightToolbarGUI);
    }

    private static void OnLeftToolbarGUI()
    {
        GUILayout.FlexibleSpace();

        if (GUILayout.Button(new GUIContent("Freddy", "Start Freddy Scene")))
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Freddy.unity");
        }

        if (GUILayout.Button(new GUIContent("Julien", "Start Julien Scene")))
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Julien.unity");
        }
    }

    private static void OnRightToolbarGUI()
    {
        if (GUILayout.Button(new GUIContent("BattlePhase / ExplorationPhase", "Start Battle or Exploration Phase")))
        {
            GameManager.Instance.OnChangePhase();
        }
    }
}
