using System.Collections;
using System.Collections.Generic;
using Com.Donut.BattleSystem;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    [SerializeField] private List<Image> actionsImage= new List<Image>();
    [SerializeField] private Text attackText;
    
    private List<Attack> _attacks = new List<Attack>();
    private int _attackIndex = 0;

    public void InitializeActionUI(List<Attack> attacks)
    {
        _attacks = attacks;
        
        if(attacks.Count > 6)
            Debug.LogError("More than 6 attack");

        for (int x = 0; x < attacks.Count; x++)
            actionsImage[x].sprite = attacks[x].iconSprite;

        attackText.text = attacks[0].attackDesc;
    }
    
    public void ShowActionUI()
    { 
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    public void HideActionUI()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    public void UpdateUI()
    {
        
    }
}
