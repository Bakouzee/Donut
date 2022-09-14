using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : Character
{
    protected CharacterType enemy;

    private void Awake()
    {
        InitCharacter(enemy);
    }
}
