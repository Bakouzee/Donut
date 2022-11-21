using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class DialogController : MonoBehaviour {

    public List<Dialog> dialogs;

    public Image authorSprite;
    public Text authorName;
    public Text dialogContent;

    public float speed;

    private Dialog currentDialog;
    private int actualPage;
    private bool isDisplayingText;

    public void StartDialog(Dialog targetDialog) {

        authorSprite.sprite = targetDialog.authorSprite;
        authorName.text = targetDialog.author;
        currentDialog = targetDialog;
        actualPage = 0;
        
        StartCoroutine(DisplayText(targetDialog, actualPage));
    }

    private IEnumerator DisplayText(Dialog targetDialog,int page) {

        for (int i = 0; i < targetDialog.pages[page].Length; i++) {
            yield return new WaitForSeconds(speed);
            isDisplayingText = true;
            dialogContent.text = targetDialog.pages[page].Substring(0, i);
        }

        isDisplayingText = false;

    }

    public void OnInteract(InputAction.CallbackContext e) {
        if (e.started && !isDisplayingText) {
            if (currentDialog.pages.Count == 1  || actualPage >= currentDialog.pages.Count - 1) {
                authorSprite.transform.parent.gameObject.SetActive(false);
                currentDialog = null;
            }
            else {
                actualPage++;
                StartCoroutine(DisplayText(currentDialog,actualPage));
            }
        }
    }
}
