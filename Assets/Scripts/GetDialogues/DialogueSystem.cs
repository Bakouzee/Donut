using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public static class DialogueSystem
{
    public static List<TextMeshProUGUI> textsToChanged = new List<TextMeshProUGUI>();

    public static void ChangeLanguage(GameManager.Language language)
    {
        string dialoguePath = Application.dataPath + "/Dialogues/dialogues.csv";
        string[] data = File.ReadAllLines(dialoguePath);

        List<string[]> lines = new List<string[]>(); // <- rows

        for(int i = 0; i < data.Length; i++)
        {
            lines.Add(data[i].Split(',')); // <- columns
        }
        switch (language)
        {
            case GameManager.Language.Français:
                for (int i = 0; i < textsToChanged.Count; i++)
                {
                    textsToChanged[i].text = lines[i + 1][1];
                }
                break;
            case GameManager.Language.English:
                for (int i = 0; i < textsToChanged.Count; i++)
                {
                    textsToChanged[i].text = lines[i + 1][2];
                }
                break;
            case GameManager.Language.Español:
                for (int i = 0; i < textsToChanged.Count; i++)
                {
                    textsToChanged[i].text = lines[i + 1][3];
                }
                break;
            default: goto case GameManager.Language.English;
        }
    }
}
