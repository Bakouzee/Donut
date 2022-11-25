
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;


public class DialogCreator : EditorWindow
{
    [MenuItem("AngelSushi/DialogCreator")]
    public static void ShowExample() {
        DialogCreator wnd = GetWindow<DialogCreator>();
        wnd.titleContent = new GUIContent("DialogCreator");
    }

    private List<VisualElement> dialogElements;

    private ObjectField authorSprite, background;

    private VisualElement parent;

    private Dialog targetDialog;

    private DateTime lastTime;

    public void CreateGUI() {
        VisualElement root = rootVisualElement;
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/Amaury/Dialogs/DialogCreatorXML.uxml");
        VisualElement labelFromUXML = visualTree.Instantiate();
        parent = labelFromUXML;

        AddMissingElements(labelFromUXML);
        GetAllDialogsElements(labelFromUXML.Q<VisualElement>("Main"));
        
        ListView dialogList = labelFromUXML.Q<ListView>("dialog-list");
        Dialog[] dialogs = Resources.LoadAll("Dialogs", typeof(Dialog)).Cast<Dialog>().ToArray();
       
        dialogList.itemsSource = dialogs;

        dialogList.onSelectionChange += OnDialogSelected;
        
        root.Add(labelFromUXML);
        
        SetDataDisplayVisible(parent,false);

        parent.Q<Button>("Save").clicked += OnSaveDatas;
        parent.Q<Button>("Add").clicked += OnCreateDialog;
        
        lastTime = DateTime.UtcNow;
        
        
        minSize = new Vector2(612, 636);
        maxSize = minSize;
    }

    private void AddMissingElements(VisualElement labelFromUXML) {
        VisualElement authorVE = labelFromUXML.Children().ToList()[1].Children().ToList()[2];
        VisualElement backgroundVE = labelFromUXML.Children().ToList()[1].Children().ToList()[7];

        authorSprite = new ObjectField();
        authorSprite.label = "Author Sprite";
        authorSprite.name = "authorSprite";
        authorSprite.objectType = typeof(Sprite);
        
        authorVE.Add(authorSprite);
       
        background = new ObjectField();
        background.label = "Background";
        background.name = "background";
        background.objectType = typeof(Sprite);
        
        backgroundVE.Add(background);

    }

    private void OnInspectorUpdate() {
        Debug.Log(DateTime.UtcNow.Millisecond);
        if (lastTime.Millisecond == DateTime.UtcNow.Millisecond - 5) {
            Debug.Log("5 milisecond passed");
            lastTime = DateTime.UtcNow;
        }
    }

    private void GetAllDialogsElements(VisualElement parent) {
        dialogElements = new List<VisualElement>();
        for(int i = 1;i < parent.childCount;i++) // 1 To skip the title visual element 
            dialogElements.Add(parent.Children().ToList()[i]);
    }

    private void SetDataDisplayVisible(VisualElement parent,bool visible) {
        
        authorSprite.parent.parent.visible = visible;
        parent.Q<VisualElement>("OtherCharacters").visible = visible;
        parent.Q<VisualElement>("DialogBackground").visible = visible;
        parent.Q<Button>("Save").visible = visible;  
    }

    private void OnDialogSelected(IEnumerable<object> selectedItems) {
        SetDataDisplayVisible(parent,true);
        
        targetDialog = (Dialog)selectedItems.First();

        TextField dialogName = (TextField)dialogElements[0].Children().ToList()[0];
        dialogName.value = targetDialog.name;

        TextField authorName = (TextField)dialogElements[1].Children().ToList()[0];
        authorName.value = targetDialog.author;

        authorSprite.value = targetDialog.authorSprite;

        TextField content = (TextField)dialogElements[3].Children().ToList()[0];
        
        string sContent = "";

        foreach (string page in targetDialog.pages)
            sContent += page + " ; ";

        content.value = sContent;
        
        Debug.Log(dialogElements[4]);

        Toggle toggle = (Toggle)dialogElements[5].Children().ToList()[0];
        toggle.value = targetDialog.isRepeatable;

    }

    private void OnSaveDatas() {
        Dialog dialog = new Dialog();
        dialog.name = ((TextField)parent.Children().ToList()[1].Children().ToList()[1].Children().ToList()[0]).value;
        dialog.author = ((TextField)parent.Children().ToList()[1].Children().ToList()[2].Children().ToList()[0]).value;
        dialog.authorSprite = (Sprite)authorSprite.value;
        dialog.isRepeatable = ((Toggle)parent.Children().ToList()[1].Children().ToList()[6].Children().ToList()[0]).value;

        string content = ((TextField)parent.Children().ToList()[1].Children().ToList()[4].Children().ToList()[0]).value;
        dialog.pages = content.Split(" ; ",content.Count(str => str == ';') + 1,StringSplitOptions.RemoveEmptyEntries).ToList();

        AssetDatabase.DeleteAsset("Assets/Resources/Dialogs/" + targetDialog.name + ".asset");
        AssetDatabase.CreateAsset(dialog,"Assets/Resources/Dialogs/" + dialog.name + ".asset");
    }

    private void OnCreateDialog() {
        Dialog newDialog = new Dialog();
        newDialog.name = "NewDialog " + Resources.LoadAll<Dialog>("Dialogs/").Length;
        AssetDatabase.CreateAsset(newDialog,"Assets/Resources/Dialogs/" + newDialog.name +  ".asset");
    }


    private IEnumerator Wait()
    {
        Debug.Log("lol");
        yield return new WaitForSeconds(2f);
        Debug.Log("lal");

        //StartCoroutine(Wait());
    }
}