using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyData : CharacterData
{
    public enum Type
    {
        Tank,
        Speeder // examples
    };

    public Type type;
}
