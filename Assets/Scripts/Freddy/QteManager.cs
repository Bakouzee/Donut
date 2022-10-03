using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QteManager : MonoBehaviour, IQte
{
    public static QteManager Instance { get; private set; }

    public float duration = 2f;
    private float durationInit;
    public float GetSetDuration
    {
        get { return duration; }
        set { duration = value; }
    }

    public int mash = 5;
    private int mashInit;
    public int GetSetMash
    {
        get { return mash; }
        set { mash = value; }
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        durationInit = GetSetDuration;
        mashInit = GetSetMash;
    }

    public void MashButton(KeyCode inputToMash)
    {
        if (Input.GetKeyDown(inputToMash))
        {
            GetSetMash--;
            if (GetSetMash <= 0)
            {
                Debug.Log("Mash was successful!");
                GetSetMash = mashInit;
            }
        }
    }

    public void MaintainButton(KeyCode inputTotMaintain)
    {
        if (Input.GetKey(inputTotMaintain))
        {
            GetSetDuration -= Time.deltaTime;
            if (GetSetDuration <= 0)
            {
                Debug.Log("Maintain was successful!");
                GetSetDuration = durationInit;
            }
        }

        if (Input.GetKeyUp(inputTotMaintain))
        {
            GetSetDuration = durationInit;
            return;
        }
    }
}
