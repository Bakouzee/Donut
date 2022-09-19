using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputControllerUI : MonoBehaviour
{
    [SerializeField] private Image input0;
    [SerializeField] private Image input1;

    [SerializeField] private Animator animInput0;
    [SerializeField] private Animator animInput1;

    public void ShowInputPlayer0()
    {
        input0.gameObject.SetActive(true);
    }
    public void ShowInputPlayer1()
    {
        input1.gameObject.SetActive(true);
    }
    
    public void HideInputPlayers()
    {
        input0.gameObject.SetActive(false);
        input1.gameObject.SetActive(false);
    }
}
