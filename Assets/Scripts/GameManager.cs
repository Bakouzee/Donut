using Com.Donut.BattleSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    #region Instance
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    [Header("Battle State")]
    public bool isBattle;
    [SerializeField] private BattleSystem battleSystem;

    #region Getter/Setter
    [SerializeField] private LayerMask layersToKeep;
    public LayerMask LayersToKeep => layersToKeep;

    [SerializeField] private LayerMask everything;
    public LayerMask Everything { get { return everything; } private set { everything = value; } }
    #endregion

    public void OnChangePhase()
    {
        if (isBattle)
        {
            battleSystem.Interface.gameObject.SetActive(true);
            Everything = Camera.main.cullingMask;
            Camera.main.cullingMask = LayersToKeep;
            isBattle = false;
            Debug.Log("BattleState");
        }
        else
        {
            battleSystem.Interface.gameObject.SetActive(false);
            Camera.main.cullingMask = Everything;
            RestoreControls();
            isBattle = true;
            Debug.Log("ExplorationState");
        }

    }

    private void RestoreControls()
    {
        battleSystem.Player.playerInput.SwitchCurrentActionMap("Player");
    }
}