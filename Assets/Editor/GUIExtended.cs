using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEditor.SceneManagement;
using UnityEngine;
//using UnityToolbarExtender;

[InitializeOnLoad]
public class GUIExtended
{
    static GUIExtended()
    {
       // ToolbarExtender.LeftToolbarGUI.Add(OnLeftToolbarGUI);
       // ToolbarExtender.RightToolbarGUI.Add(OnRightToolbarGUI);
    }

    private static void OnLeftToolbarGUI()
    {
        GUILayout.FlexibleSpace();

        if (GUILayout.Button(new GUIContent("LD1", "Start FinalLD Scene")))
        {
            EditorSceneManager.OpenScene("Assets/Scenes/FinalLD.unity");
        }

        if (GUILayout.Button(new GUIContent("Freddy", "Start Freddy Scene")))
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Freddy.unity");
        }

        if (GUILayout.Button(new GUIContent("Julien", "Start Julien Scene")))
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Julien.unity");
        }

        if (GUILayout.Button(new GUIContent("Build", "Build Game")))
        {
            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
            buildPlayerOptions.scenes = new[] { "Assets/Scenes/FinalLD.unity"};
            buildPlayerOptions.locationPathName = "Builds/GameBuildScript.exe";
            buildPlayerOptions.target = BuildTarget.StandaloneWindows;
            buildPlayerOptions.options = BuildOptions.None;

            BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
            BuildSummary summary = report.summary;

            if (summary.result == BuildResult.Succeeded)
            {
                Debug.Log("Build succeeded: " + summary.totalSize + " bytes");
            }

            if (summary.result == BuildResult.Failed)
            {
                Debug.Log("Build failed");
            }
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
