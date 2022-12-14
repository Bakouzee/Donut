using Cinemachine;
using Com.Donut.BattleSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameManager : SingletonBase<GameManager>
{
    [SerializeField] private SaveGameController saveController;
    [SerializeField] private Animator doorCinematic;
    private Player player;
    [SerializeField] private GameObject minimap;
    [SerializeField] private CinemachineVirtualCamera camPlayer;
    [SerializeField] private List<TextMeshProUGUI> textsLoc = new List<TextMeshProUGUI>();
   
    public enum Language
    {
        Francais,
        English,
        Espanol
    }
    
    [SerializeField] private Language language;

    [Header("Tuto")]
    public TextMeshProUGUI tutoText;
    public string shellDroped;
    public string shellInput;

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

        if (tutoText != null)
        {
            tutoText.transform.parent.gameObject.SetActive(false);
            tutoText.text = shellDroped;
        }

        player = FindObjectOfType<Player>();

        DontDestroyOnLoad(saveController.gameObject);
        DialogueSystem.textsToChanged.AddRange(textsLoc);
        DialogueSystem.ChangeLanguage(language);
    }

    public IEnumerator PlayDoorCinematic()
    {
        //camPlayer.m_Follow = null;
        player.playerInput.DeactivateInput();
        doorCinematic.SetTrigger("Door");
        yield return new WaitForSeconds(4f);
        //camPlayer.m_Follow = player.transform;
        player.playerInput.ActivateInput();
    }

    public void OnChangePhase()
    {
        if (isBattle)
        {
            minimap.SetActive(false);
            battleSystem.BattleUI.gameObject.SetActive(true);
            Everything = Camera.main.cullingMask;
            Camera.main.cullingMask = LayersToKeep;
            isBattle = false;
            
            AudioManager.Instance.MusicAudioSource.enabled = false;
        }
        else
        {
            if (player.shellToTake != null)
            {
                player.shellToTake.SetActive(true);
                tutoText.transform.parent.gameObject.SetActive(true);
            }
            minimap.SetActive(true);
            battleSystem.BattleUI.gameObject.SetActive(false);
            Camera.main.cullingMask = Everything;
            RestoreControls();
            isBattle = true;
            
            AudioManager.Instance.MusicAudioSource.enabled = true;
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