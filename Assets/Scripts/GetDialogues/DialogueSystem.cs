using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public static class DialogueSystem
{
    private static string language = "Fran�ais";

    public static void ShowDataPath()
    {
        string dialoguePath = Application.dataPath + "/Dialogues/dialogues.csv";
        string[] data = File.ReadAllLines(dialoguePath);

        List<string[]> lines = new List<string[]>(); // <- rows

        for(int i = 0; i < data.Length; i++)
        {
            lines.Add(data[i].Split(',')); // <- columns
        }

        if (language == "Fran�ais")
        {
            //Volume
            Debug.Log(lines[1][1]);

            //General
            Debug.Log(lines[2][1]);

            //Musique
            Debug.Log(lines[3][1]);

            //Effets
            Debug.Log(lines[4][1]);
        } else if(language == "English")
        {
            Debug.Log(lines[1][2]);
        }
        else if(language == "Espa�ol")
        {
            Debug.Log(lines[1][3]);
        }
    }
}
