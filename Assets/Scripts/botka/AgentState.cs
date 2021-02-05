using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentState : MonoBehaviour
{
    
    public Transform _AgentTransform;
    public bool _Lock;
    public bool _PosLock;

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
        if (_Lock){
            _PosLock = _Lock;
        }
    }
}
