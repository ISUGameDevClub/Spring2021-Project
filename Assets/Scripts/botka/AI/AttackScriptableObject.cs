using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Attack", order = 1)]
public class AttackScriptableObject : ScriptableObject
{
    public string AttackName;
    public string AttackDescription;
    [Range(0f, 1000f)]public float AttackRange;
    [Range(0f, 1000f)]public float AttackSpeed;
}
