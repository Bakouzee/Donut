using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterType", menuName = "Enemy", order = 1)]
public class EnemyType : ScriptableObject
{
    public List<EnemyData> Enemy;
}
