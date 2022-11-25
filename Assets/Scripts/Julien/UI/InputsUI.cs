using System.Collections;
using System.Collections.Generic;
using Com.Donut.BattleSystem;
using UnityEngine;
using UnityEngine.UI;

public class InputsUI : MonoBehaviour
{
    [Header("DisplayInput_UI")]
    [SerializeField] private GameObject input0;
    [SerializeField] private GameObject input1;

    [SerializeField, NonReorderable] private List<Inputs> listPlayerInputsOnEnemy = new List<Inputs>();

    private BattleSystem _battleSystem;

    [System.Serializable]
    public class Inputs
    {
        public GameObject input0;
        public GameObject input1;
        public List<GameObject> listInputArrow = new List<GameObject>();
    }

    public void InitializeInputsUI(BattleSystem battleSystem)
    {
        _battleSystem = battleSystem;
    }

    public void SetActiveInputOnPlayer(FighterData fighterData, bool result)
    {
        if(fighterData == _battleSystem.ListPlayersData[0])
            input0.SetActive(result);
        else
            input1.SetActive(result);
    }

    public void SetActiveInputOnEnemy(FighterData fighterData, bool result)
    {
        if (BattleSystem.CurrentFighterData == _battleSystem.ListPlayersData[0])
        {
            listPlayerInputsOnEnemy[fighterData.ID].input0.SetActive(result);
            foreach(GameObject go in listPlayerInputsOnEnemy[fighterData.ID].listInputArrow)
            {
                go.SetActive(result);
            }
        }
        else
        {
            listPlayerInputsOnEnemy[fighterData.ID].input1.SetActive(result);
            foreach(GameObject go in listPlayerInputsOnEnemy[fighterData.ID].listInputArrow)
            {
                go.SetActive(result);
            }
        }
           
    }
}
