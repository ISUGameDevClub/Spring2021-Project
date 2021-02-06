using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @Author Jake Botka
 * 
 */
public class AgentBehavior : MonoBehaviour
{
    public const Behaviour DefaultBehvaiour = Behaviour.Halt;
    public const Action DefaultAction = Action.Cease;
    public const BehavioralAggression DefaultAggression = BehavioralAggression.Passive;
    public Behaviour StartingBehavior;
    public Action StartingAction;
    public BehavioralAggression StartingAggression;
    [Range(0f,1000f)] public float StopingDistance;
    public enum Behaviour 
    {
        Null,Persue, Halt, Action
    }

    public enum Action
    {
        Null,Patrol, Attack, Cease
    }
    public enum BehavioralAggression 
    {
        Null,Passive, Moderate, Agressive
    } 
    public GameObject Target;
    
    [Header("Target Information - DO NOT SET")]
    public Transform TargetTransform;

    public bool Persue;
    public bool LogEvents;

    void Awake()
    {
        if (StartingBehavior == Behaviour.Null)
        {
            UnityLoggingDelegate.LogIfTrue(LogEvents, UnityLoggingDelegate.LogType.Warning, "Default Starting Behavior was applied in script due to the lack of application in the editor");
            StartingBehavior = DefaultBehvaiour;
        }
        if (StartingAction == Action.Null)
        {
            UnityLoggingDelegate.LogIfTrue(LogEvents, UnityLoggingDelegate.LogType.Warning, "Default Starting Action was applied in script due to the lack of application in the editor");
            StartingAction = DefaultAction;
        }
        if (StartingAggression == BehavioralAggression.Null)
        {
            UnityLoggingDelegate.LogIfTrue(LogEvents, UnityLoggingDelegate.LogType.Warning, "Default Satarting behvaioural aggression was applied in script due to the lack of application in the editor");
            StartingAggression = DefaultAggression;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {

    }


    void ExecuteBehvaior(Behaviour behaviour)
    {

    }

    void ExecuteAction(Action action)
    {

    }
    
    void ApplyAggression(BehavioralAggression aggression)
    {

    }

    void ExecutePatrolAction()
    {

    }

    void ExecuteCeaseAction()
    {

    }

    void ExecuteAttackAction()
    {

    }

    void ExecuteAgentDeath()
    {
        GameObject.Destroy(gameObject); //Should be last call of this meathod.
        UnityLoggingDelegate.LogIfTrue(LogEvents, UnityLoggingDelegate.LogType.General, "AI Agent: " + gameObject.transform.root.gameObject.name + " has been destroyed in scene");
    }

    
}
