using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MinimapController : MonoBehaviour {
    public bool isInMap;

    public static MinimapController instance { get; private set; }

    public GameObject minimap;
    public GameObject map;
    
    void Awake() {
        instance = this;
    }

    public void OnMinimapManage(InputAction.CallbackContext e) {
        if (e.performed) {
            isInMap = !isInMap;
            
            map.SetActive(isInMap);
            minimap.SetActive(!isInMap);
        }
    }
}
