using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Dialog", menuName = "Dialogs/Dialog", order = 1)]
public class Dialog : ScriptableObject
{

    public string name;
    public string author;
    public Sprite authorSprite;
    public bool isRepeatable;
    public List<string> pages;
    public List<Texture2D> otherCharacters;
    

}
