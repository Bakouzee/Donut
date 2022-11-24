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
            isInMap = !isInMap;
            minimap.SetActive(!isInMap);
            map.SetActive(isInMap);
        }
    }
}
