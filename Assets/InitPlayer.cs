using System;
using System.Collections;
using System.Collections.Generic;
using Com.Donut.BattleSystem;
using UnityEngine;

public class InitPlayer : MonoBehaviour
{

    private GameObject _player;
    [SerializeField] private BattleSystem battlesystem;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _player.GetComponent<Player>().battleSystem = this.battlesystem;
    }
    
}
