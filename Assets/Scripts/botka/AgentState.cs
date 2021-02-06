using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @Author Jake Botka
 * 
 */
public class AgentState : MonoBehaviour
{
    public enum EntityStatus
    {
        None, NotSpawned, Dead, Patrol, Idle, PursuingTarget, Wandering
    }
    
    public Transform _AgentTransform;
    public bool Lock;
    public bool PosLock;

    public EntityStatus entityStatus;
    public  bool canMove; //variable determines if AI is locked in position
    public bool canAttack;
    public bool fixedPathing;
    public bool variablePathing;
    public bool LogEvents;

 /*
    public bool _Lock 
    {
        get{return _Lock;}
        set
        {
            _Lock = value;
            if (_Lock){
                _PosLock = true;
            }
        }
    }
    */
    void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (Lock){
            PosLock = Lock;
        }
    }
}
