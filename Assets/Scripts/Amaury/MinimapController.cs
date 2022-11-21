using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MinimapController : MonoBehaviour {
    public bool isInMap;

    public static MinimapController instance { get; private set; }

    public GameObject minimap;
    public GameObject map;

    public RenderTexture[] renders;

    public Image test;

    void Awake() {
        instance = this;
        
        
    }

    public void OnMinimapManage(InputAction.CallbackContext e) {
        if (e.performed) {
          /*  isInMap = !isInMap;
            
            map.SetActive(isInMap);
            minimap.SetActive(!isInMap);
            */
          List<Texture2D> allTexs = new List<Texture2D>();
          
          foreach (RenderTexture rTex in renders) {
              
              Texture2D tex = new Texture2D(rTex.width,  rTex.height, TextureFormat.RGB24, false);
              RenderTexture.active = rTex;
              tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
              
              tex.Apply();
              
              allTexs.Add(tex);
          }

          List<Color> allColors = new List<Color>();

          foreach (Texture2D tex in allTexs) {
              foreach(Color color in tex.GetPixels())
                  allColors.Add(color);
          }
          
//          Texture2D texture = new Texture2D()


          // Sprite sprite = Sprite.Create(tex, new Rect(new Vector2(0,0), new Vector2(1024* renders.Length, 1024 * renders.Length)), new Vector2(0.5f, 0.5f));
          // test.sprite = sprite;
        }
    }
}
