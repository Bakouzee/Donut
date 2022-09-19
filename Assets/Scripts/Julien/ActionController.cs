using System.Collections;
using System.Collections.Generic;
using Com.Donut.BattleSystem;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    [SerializeField] private List<Image> actionsImage= new List<Image>();
    [SerializeField] private Text abilityText;
    
    [SerializeField] List<Abilities> abilities = new List<Abilities>();
    private Abilities _abilityIndex;

    public void InitializeActionUI(List<Abilities> abilitiesRef)
    {
        abilities = abilitiesRef;
        
        if(abilities.Count > 6)
            Debug.LogError("More than 6 attack");

        for (int x = 0; x < abilities.Count; x++)
            actionsImage[x].sprite = abilities[x].iconSprite;

        _abilityIndex = abilities[0];
        abilityText.text = _abilityIndex.attackDesc;
        
        Debug.Log("End of Initialize");
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
        if (abilities.Count <= 1) return;

        var attackRef = abilities[0];
        abilities.Remove(abilities[0]);
        abilities.Add(attackRef);
        
        for (int x = 0; x < abilities.Count; x++)
            actionsImage[x].sprite = abilities[x].iconSprite;
        
        _abilityIndex = abilities[0];
        abilityText.text = _abilityIndex.attackDesc;
    }
}
