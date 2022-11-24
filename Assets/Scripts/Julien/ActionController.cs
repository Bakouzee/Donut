using System.Collections.Generic;
using Com.Donut.BattleSystem;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    private BattleSystem _battleSystem;

    [Header("Ability Player0")] [SerializeField]
    private List<Image> actionsImagePlayer0 = new List<Image>();

    [SerializeField] private Text abilityTextPlayer0;
    [SerializeField] List<Abilities> listAbilitiesPlayer0 = new List<Abilities>();
    private Abilities _abilityIndexPlayer0;
    [SerializeField] private Transform abilityParentPlayer0;

    private Animator _animPlayer0;

    [Header("Ability Player1")] [SerializeField]
    private List<Image> actionsImagePlayer1 = new List<Image>();

    [SerializeField] private Text abilityTextPlayer1;
    [SerializeField] List<Abilities> listAbilitiesPlayer1 = new List<Abilities>();
    private Abilities _abilityIndexPlayer1;
    [SerializeField] private Transform abilityParentPlayer1;
    
    private Animator _animPlayer1;

    public void InitializeActionUI(List<Abilities> abilitiesPlayer0, List<Abilities> abilitiesPlayer1, BattleSystem battleSystem)
    {
        _battleSystem = battleSystem;

        listAbilitiesPlayer0 = abilitiesPlayer0;
        listAbilitiesPlayer1 = abilitiesPlayer1;

        if (listAbilitiesPlayer0.Count > 6 || listAbilitiesPlayer1.Count > 6)
            Debug.LogError("More than 6 attack");

        for (int x = 0; x < listAbilitiesPlayer0.Count; x++)
            actionsImagePlayer0[x].sprite = listAbilitiesPlayer0[x].iconSprite;

        _abilityIndexPlayer0 = listAbilitiesPlayer0[0];
        abilityTextPlayer0.text = _abilityIndexPlayer0.attackDesc;

        for (int y = 0; y < listAbilitiesPlayer1.Count; y++)
            actionsImagePlayer1[y].sprite = listAbilitiesPlayer1[y].iconSprite;
        
        _abilityIndexPlayer1 = listAbilitiesPlayer1[0];
        abilityTextPlayer1.text = _abilityIndexPlayer1.attackDesc;
    }

    public void InitializeAnimator(Animator anim0, Animator anim1)
    {
        _animPlayer0 = anim0;
        _animPlayer1 = anim1;
    }

    public void SetActiveAbility_UI(FighterData fighterData, bool result)
    {
        if (fighterData == _battleSystem.ListPlayersData[0])
        {
            foreach (Transform child in abilityParentPlayer0)
            {
                child.gameObject.SetActive(result);
            }
        }
        else
        {
            foreach (Transform child in abilityParentPlayer1)
            {
                child.gameObject.SetActive(result);
            }
        }
    }

    public Abilities UpdateCurrentAbility(FighterData fighterData)
    {
        if (fighterData == _battleSystem.ListPlayersData[0])
        {
            if (listAbilitiesPlayer0.Count <= 1) Debug.LogError("Error actioncontroller");

            var attackRef = listAbilitiesPlayer0[0];
            listAbilitiesPlayer0.Remove(listAbilitiesPlayer0[0]);
            listAbilitiesPlayer0.Add(attackRef);

            for (int x = 0; x < listAbilitiesPlayer0.Count; x++)
                actionsImagePlayer0[x].sprite = listAbilitiesPlayer0[x].iconSprite;

            _abilityIndexPlayer0 = listAbilitiesPlayer0[0];
            abilityTextPlayer0.text = _abilityIndexPlayer0.attackDesc;
            return _abilityIndexPlayer0;
        }
        else
        {
            if (listAbilitiesPlayer1.Count <= 1) Debug.LogError("Error actioncontroller");;

            var attackRef = listAbilitiesPlayer1[0];
            listAbilitiesPlayer1.Remove(listAbilitiesPlayer1[0]);
            listAbilitiesPlayer1.Add(attackRef);

            for (int x = 0; x < listAbilitiesPlayer1.Count; x++)
                actionsImagePlayer1[x].sprite = listAbilitiesPlayer1[x].iconSprite;

            _abilityIndexPlayer1 = listAbilitiesPlayer1[0];
            abilityTextPlayer1.text = _abilityIndexPlayer1.attackDesc;
            return _abilityIndexPlayer1;
        }
    }

    public void LaunchAbility(FighterData fighterData)
    {
        if (fighterData.CurrentAbility.actionType == Abilities.ActionType.Escape)
        {
            SetCurrentAbilityToEscape();
            return;
        }

        if (fighterData == _battleSystem.ListPlayersData[0])
        {
            LaunchAbiltiyPlayer0(fighterData);
        }
        else if (fighterData == _battleSystem.ListPlayersData[1])
        {
            LaunchAbiltiyPlayer1(fighterData);
        }
    }

    public void ResetAnimator()
    {
        _animPlayer0.runtimeAnimatorController = _battleSystem.ListPlayersData[0].Fighter.AnimatorController;
        _animPlayer1.runtimeAnimatorController = _battleSystem.ListPlayersData[1].Fighter.AnimatorController;
    }

    private void SetCurrentAbilityToEscape()
    {
        var abilityPlayer0 = ScriptableObject.CreateInstance<Abilities>();
        
        foreach (Abilities abilities in _battleSystem.ListPlayersData[0].Fighter.Abilities)
        {
            if (abilities.actionType != Abilities.ActionType.Escape) continue;
            abilityPlayer0 = abilities;
            Debug.Log("Player0 :" + abilities.name);
        }
        _battleSystem.ListPlayersData[0].SetFighterCurrentAbility(abilityPlayer0);
        
        var abilityPlayer1 = ScriptableObject.CreateInstance<Abilities>();
        
        foreach (Abilities abilities in _battleSystem.ListPlayersData[1].Fighter.Abilities)
        {
            if (abilities.actionType != Abilities.ActionType.Escape) continue;
            abilityPlayer1 = abilities;
            Debug.Log("Player1 :" + abilities.name);
        }
        _battleSystem.ListPlayersData[1].SetFighterCurrentAbility(abilityPlayer1);
        
        LaunchAbiltiyPlayer0(_battleSystem.ListPlayersData[0]);
        LaunchAbiltiyPlayer1(_battleSystem.ListPlayersData[1]);
    }

    public Abilities LaunchEnemyAbility(FighterData fighterData)
    {
        int rand = Random.Range(0, fighterData.Fighter.Abilities.Count);
        var triggerName = fighterData.Fighter.Abilities[rand].attackName;
        _battleSystem.BattleUI.SetAnimTrigger(fighterData, triggerName);
        return fighterData.Fighter.Abilities[rand];
    }

    private void LaunchAbiltiyPlayer0(FighterData fighterData)
    {
        _animPlayer0.runtimeAnimatorController = _abilityIndexPlayer0._animatorController;
        var triggerName = _abilityIndexPlayer0.attackName;
        _battleSystem.BattleUI.SetAnimTrigger(fighterData, triggerName);
    }
    
    private void LaunchAbiltiyPlayer1(FighterData fighterData)
    {
        _animPlayer1.runtimeAnimatorController = _abilityIndexPlayer1._animatorController;
        var triggerName = _abilityIndexPlayer1.attackName;
        _battleSystem.BattleUI.SetAnimTrigger(fighterData, triggerName);
    }
}