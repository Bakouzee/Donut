using Cinemachine;
using Com.Donut.BattleSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameManager : SingletonBase<GameManager>
{
    [SerializeField] private SaveGameController saveController;
    [SerializeField] private Animator doorCinematic;
    [SerializeField] private Player player;
    [SerializeField] private CinemachineVirtualCamera camPlayer;

    [Header("Battle State")]
    public bool isBattle;
    [SerializeField] private BattleSystem battleSystem;

    #region Getter/Setter
    [SerializeField] private LayerMask layersToKeep;
    public LayerMask LayersToKeep => layersToKeep;

    [SerializeField] private LayerMask everything;
    public LayerMask Everything { get { return everything; } private set { everything = value; } }

    private bool isPaused;
    public bool IsPaused { get { return isPaused; } private set { isPaused = value; } }
    #endregion

    protected override void Awake()
    {
        base.Awake();
        DialogueSystem.ShowDataPath();
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "GrotteLD")
        {
            StartCoroutine(PlayDoorCinematic());
        }
    }

    private IEnumerator PlayDoorCinematic()
    {
        camPlayer.m_Follow = null;
        player.playerInput.DeactivateInput();
        doorCinematic.SetTrigger("Door");
        yield return new WaitForSeconds(4f);
        camPlayer.m_Follow = player.transform;
        player.playerInput.ActivateInput();
    }

    public void OnChangePhase()
    {
        if (isBattle)
        {
            battleSystem.BattleUI.gameObject.SetActive(true);
            Everything = Camera.main.cullingMask;
            Camera.main.cullingMask = LayersToKeep;
            isBattle = false;
            Debug.Log("BattleState");
        }
        else
        {
            battleSystem.BattleUI.gameObject.SetActive(false);
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

    public void OnPause(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            IsPaused = !isPaused;
            if (IsPaused)
            {
                Time.timeScale = 0;
                AudioManager.Instance.audioMenu.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                AudioManager.Instance.audioMenu.SetActive(false);
            }
        }
    }

    public void OnSave(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            SaveGame();
    }

    public void OnLoad(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            LoadGame();
    }

    public void SaveGame()
    {
        saveController.OnSave();
    }
    public void LoadGame()
    {
        saveController.OnLoad();
    }
}