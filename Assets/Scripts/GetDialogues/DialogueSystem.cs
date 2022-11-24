using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using static System.Net.WebRequestMethods;
using System.Collections;
using UnityEditor;

public static class DialogueSystem
{
    private const string sheetID = "1ACsLJEZe0o00_fdj_82lnPHPp2oDGH9wiXPoadmvffg";
    private const string link = "https://docs.google.com/spreadsheets/d/" + sheetID + "/export?format=csv";

    internal static IEnumerator DlData()
    {
        Debug.Log("in DLDATA");

        yield return new WaitForEndOfFrame();
        Debug.Log("after DLDATA");

        using (UnityWebRequest webRequest = UnityWebRequest.Get(link))
        {
            Debug.Log("in using");
            yield return webRequest.SendWebRequest();
            Debug.Log("Success");
            Debug.Log("Data :" + webRequest.downloadHandler.text);
            string path = Application.persistentDataPath + "/Dialogues/" + webRequest.downloadHandler;
            if(!Directory.Exists(Application.persistentDataPath + "/Dialogues"))
                Directory.CreateDirectory(Application.persistentDataPath + "/Dialogues");

            System.IO.File.Create(path);
            string results = webRequest.downloadHandler.text;
            Debug.Log(results);
        }
    }
}
