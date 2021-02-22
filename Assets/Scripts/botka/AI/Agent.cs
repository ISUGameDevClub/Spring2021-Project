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

    public GameObject MainTarget;
    
    
    
    public bool LogEvents;
    public bool DeepLogEvents;

    private bool _NoRun;

    void Awake()
    {
        InitAgent(out _NoRun);
    }
    
    void Start()
    {
        if (MainTarget == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            //this is redundant i can just assign maintarget to player but i wanted to show how to use this assignment statement as its useful.
            MainTarget = player != null ? player : null; 
        }
        if (_NoRun)
        {
            Debug.LogError("Script halted Due to error. Check error logs");
            gameObject.SetActive(false);
        }
        AgentBehavior.AgentStateScript = AgentState;
        
    }

    void FixedUpdate()
    {
        AgentBehavior.Target = MainTarget;
        
    }

    [ContextMenu("Bind persue to Main Target")]
    public void PersueTarget()
    {
        PersueTarget(MainTarget);
    }

    
    public void PersueTarget(GameObject target)
    {
        if (target != null)
        {
            AgentBehavior.Target = target;
            AgentBehavior.ExecuteAction(AgentBehavior.Action.Persue, MainTarget, true);
        }
    }

    private void InitAgent(out bool noRun)
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

    public void InitDeath()
    {
        UnityLoggingDelegate.LogIfTrue(LogEvents, UnityLoggingDelegate.LogType.General, "AI Agent: " + transform.root.gameObject.name + " has been destroyed in scene");
        DestoryAgent();
    }

    public void DestoryAgent()
    {
        Destroy(gameObject);
        UnityLoggingDelegate.LogIfTrue(LogEvents, UnityLoggingDelegate.LogType.General, "AI Agent Gameobject has been destroyed");
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        UnityLoggingDelegate.LogIfTrue(LogEvents, UnityLoggingDelegate.LogType.General,"Collision entered with: " + other.gameObject.name);
    }

    private void OnCollisionStay2D(Collision2D other) 
    {
        UnityLoggingDelegate.LogIfTrue(DeepLogEvents, UnityLoggingDelegate.LogType.General, "Collision on stay with: " + other.gameObject.name);
    }

    private void OnCollisionExit2D(Collision2D other) 
    {
         UnityLoggingDelegate.LogIfTrue(LogEvents, UnityLoggingDelegate.LogType.General, "Collision exited with: " + other.gameObject.name);
    }

    
}
