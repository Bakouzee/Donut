using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class DebugMenuBattlePhase : EditorWindow
{
    [MenuItem("Tools/Debug Menu/Battle Phase")]
    static void InitDebugMenu()
    {
        DebugMenuBattlePhase window = GetWindow<DebugMenuBattlePhase>();
        window.titleContent = new GUIContent("Debug Menu : Battle Phase");
        window.Show();
    }
    private void OnGUI()
    {
        
    }
}
