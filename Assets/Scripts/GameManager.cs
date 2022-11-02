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
    [SerializeField] private LayerMask layersToKeep;

    #region Getter/Setter
    public LayerMask LayersToKeep => layersToKeep;

    private LayerMask everything;
    public LayerMask Everything { get { return everything; } private set { everything = value; } }
    #endregion

    public void OnChangePhase(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (isBattle)
            {
                Everything = Camera.main.cullingMask;
                Camera.main.cullingMask = LayersToKeep;

                battleSystem.SetState(new Init(battleSystem));
                Debug.Log("BattleState");
            } else
            {
                battleSystem.Interface.gameObject.SetActive(false);
                Camera.main.cullingMask = Everything;
                RestoreControls();
                Debug.Log("ExplorationState");
            }
            isBattle = !isBattle;
        }
    }

    private void RestoreControls()
    {
        battleSystem.Player.playerInput.SwitchCurrentActionMap("Player");
    }
}