using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Attack", order = 1)]
public class AttackScriptableObject : ScriptableObject
{
    public enum AttackType
    {
        Ranged,Meele
    }
    public string AttackName;
    [TextArea]public string AttackDescription;

    public AttackType TypeOfAttack;

    [Range(0f, 1000f)]public float AttackRange;
    [Range(0f, 1000f)]public float AttackSpeed;
    [Range(0f,1000f)]public float AttackDamage;
    [Range(0f,10f)]public float AttackDamageModifier;
}
