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
        BattleSystem.CanUseInput = result;

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
        if (fighterData == _battleSystem.ListPlayersData[0])
        {
            _animPlayer0.runtimeAnimatorController = _abilityIndexPlayer0._animatorController;
            var triggerName = _abilityIndexPlayer0.attackName;
            _battleSystem.Interface.SetAnimTrigger(fighterData, triggerName);
        }
        else if (fighterData == _battleSystem.ListPlayersData[1])
        {
            _animPlayer1.runtimeAnimatorController = _abilityIndexPlayer1._animatorController;
            var triggerName = _abilityIndexPlayer1.attackName;
            _battleSystem.Interface.SetAnimTrigger(fighterData, triggerName);
        }
    }

    public void ResetAnimator()
    {
        _animPlayer0.runtimeAnimatorController = _battleSystem.ListPlayersData[0].fighter.AnimatorController;
        _animPlayer1.runtimeAnimatorController = _battleSystem.ListPlayersData[1].fighter.AnimatorController;
    }

    public Abilities LaunchEnemyAbility(FighterData fighterData)
    {
        int rand = Random.Range(0, fighterData.fighter.Abilities.Count);
        var triggerName = fighterData.fighter.Abilities[rand].attackName;
        _battleSystem.Interface.SetAnimTrigger(fighterData, triggerName);
        return fighterData.fighter.Abilities[rand];
    }
}