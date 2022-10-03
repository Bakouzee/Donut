using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{
    private CharacterType player;
    private void Awake()
    {
        InitCharacter(player);
    }

    protected override void Move()
    {
        throw new System.NotImplementedException();
    }

}
