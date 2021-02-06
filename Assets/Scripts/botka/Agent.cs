using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @Author Jake Botka
 * 
 */
public class Agent : MonoBehaviour
{
    public AgentState AgentState;
    public AgentBehavior AgentBehavior;
    public GameObject AgentObj;
    
    
    
    public bool LogEvents;

    private bool _NoRun;

    void Awake()
    {
        InitAgent(out _NoRun);
        
    }
    
    void Start()
    {
        if (_NoRun)
        {
            Debug.LogError("Script halted Due to error. Check error logs");
            gameObject.SetActive(false);
        }
    }

    void FixedUpdate()
    {

    }

    void InitAgent(out bool noRun)
    {
        //Out modifier allows you to pass refrence instead of value meaning if refrence or value of local variable is changed then it is also reflected in the global variable. 
        // This does not occur in java. This is technically not needed as I can refrence _NoRun with the current scope. 
        //iteHowever I wanted to demostrated hwo to change source object frence if this method was in a seperate class. Variosu unity functions utalize this.
        noRun = false; // this changes the passed variable too which in this case is _NoRun.
        if (AgentObj == null)
        {
            AgentObj = gameObject;
            if (AgentObj == null){
                Debug.LogError("Agent script not connected to an AI agent gameobject");
            }
        }
        if (AgentObj.tag != "AI-Agent")
        {
            Debug.LogError("Agent script not connected to an AI agent gameobject");
            noRun = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (LogEvents)
        {
            Debug.Log("Collision entered with: " + other.gameObject.name);
        }
    }

    private void OnCollisionStay2D(Collision2D other) 
    {
        if (LogEvents)
        {
            Debug.Log("Collision on stay with: " + other.gameObject.name);
        }
    }

    private void OnCollisionExit2D(Collision2D other) 
    {
        if (LogEvents)
        {
            Debug.Log("Collision exited with: " + other.gameObject.name);
        }
    }

    
}
