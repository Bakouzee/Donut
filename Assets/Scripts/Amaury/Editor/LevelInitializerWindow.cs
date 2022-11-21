using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelInitializerWindow : EditorWindow  {
    
    [MenuItem("Tools/LevelInitializer")]
    public static void ShowExample() {
        LevelInitializerWindow wnd = GetWindow<LevelInitializerWindow>();
        wnd.titleContent = new GUIContent("LevelInitializer");
    }

    public void CreateGUI() {
        maximized = true;
        
        
    }
}
