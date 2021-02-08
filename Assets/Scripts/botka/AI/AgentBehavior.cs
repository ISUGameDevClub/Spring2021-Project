using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * @Author Jake Botka
 * 
 */
public class AgentBehavior : MonoBehaviour
{
    
    public const Action DefaultAction = Action.Halt;
    public const BehavioralAggression DefaultAggression = BehavioralAggression.Passive;
    
    public Action StartingAction;
    public BehavioralAggression StartingAggression;

    [Header("Attack Options")]
    public float DistanceToAttack;
    public AttackScriptableObject[] SupportedAttacks;
    

    [Header("Patrol options")]
    public Transform[] PatrolPoints;
    [Range(0f,100f)]public float VelocityX;
    [Range(0f, 100f)]public float VelocityY;
    [Range(0f,100f)] public float StopingDistance;

    [HideInInspector]public float DistanceToTarget;

    private Coroutine _MoveCoroutine;
    private List<WrappedAction> _ActiveActionRoutines;
    private Coroutine _AttackCoroutine;
    
    

    public enum Action
    {
        Null,Patrol, Attack, Persue, Halt, 
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
        _ActiveActionRoutines = new List<WrappedAction>(0);
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

        if (Target != null)
        {
            Target = Target.transform.root.gameObject;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private List<int> _PendingRemovals = new List<int>(0);
    void FixedUpdate()
    {
        int index = 0;
        foreach(WrappedAction wrappedAction in _ActiveActionRoutines)
        {
            //second condiction done == true will not execute if the first condition wrappedaction != null is true thus being null safe
            if (wrappedAction != null && wrappedAction.Done == true)
            {
                StopCoroutine(wrappedAction.ActionCoroutineProp);
                _PendingRemovals.Add(index);

            }
            index++;
        }
        foreach(int i in _PendingRemovals)
        {
            UnityLoggingDelegate.LogIfTrue(LogEvents, UnityLoggingDelegate.LogType.General, "Completed action removed");
            _ActiveActionRoutines.RemoveAt(i);
        }
        //Basic checking
        if ( _AttackCoroutine == null && DistanceToTarget <= DistanceToAttack)
        {
            ExecuteAction(Action.Attack, Target);
        }
    }

    


    void ApplyAggression(BehavioralAggression aggression)
    {

    }

    bool HasActiveCoroutines(Action action)
    {
        //If the List has no elements it will skip this loop.
        foreach(WrappedAction wrappedAction in _ActiveActionRoutines)
        {
            if (wrappedAction != null)
            {
                if (wrappedAction.ActionProp == action)
                {
                    return true;
                }
            }
        }
        
        return false;
    }
    bool HasActiveCoroutines(Coroutine coroutine)
    {
        if (coroutine != null)
        {
            //If the List has no elements it will skip this loop.
            foreach(WrappedAction wrappedAction in _ActiveActionRoutines)
            {
                if (wrappedAction != null)
                {
                    if (wrappedAction.ActionCoroutineProp.Equals(coroutine))
                    {
                        return true;
                    }
                }
            }
        }
        
        return false;
    }
    

    void ExecuteAction(Action action)
    {
        ExecuteAction(action, Target);
    }

    void ExecuteAction(Action action, GameObject target, bool overrideAction, Action[] overridenActions)
    {
        if (_ActiveActionRoutines.Count > 0) 
        {
            if (overrideAction == true)
            {
                int index = 0;
                foreach (WrappedAction wrappedAction in _ActiveActionRoutines)
                {
                    if (wrappedAction != null)
                    {
                        foreach(Action action1 in  overridenActions)
                        {
                            if (wrappedAction.ActionProp == action1)
                            {
                                StopActiveAction(wrappedAction);
                                _ActiveActionRoutines.RemoveAt(index);
                                break;
                            }
                        }
                    }
                    index++;
                }
                
            }
            
        }
        else
        {
            ExecuteAction(action, target);
        }
        
    }
    void ExecuteAction(Action action, GameObject targetOfAction)
    {
        Coroutine c = null;
        WrappedAction w = w = new WrappedAction(null,action, targetOfAction, false);
        switch(action)
        {
            
            case Action.Attack:
                c = StartCoroutine(ExecuteAttackAction(w,targetOfAction));
                break;
            case Action.Patrol:
                c = StartCoroutine(ExecutePatrolAction(w,PatrolPoints));
                break;
            case Action.Halt:
                c = StartCoroutine(ExecutePatrolAction(w,PatrolPoints));
                break;
            case Action.Persue:
                c = StartCoroutine(ExecutePatrolAction(w,PatrolPoints));
                break;
            default:
                break;
        }

        if (w != null)
        {
            w.ActionCoroutineProp = c;
            _ActiveActionRoutines.Add(w);
        }
        
    }



    [ContextMenu("Begin patrol")]
    IEnumerator ExecutePatrolAction(WrappedAction action, Transform[] patrolPoints)
    {
        if (patrolPoints != null )
        {
           if (HasActiveCoroutines(Action.Patrol) == false)
           {

           }
           else
           {
                Debug.LogError("Attemting to Start patrol action when already patrolling");
           }
        }
        else
        {
            Debug.LogError("Attemting to patrol but no patrol points are provided");
        }
        yield return new WaitForEndOfFrame();
        action.Done = true;
        
    }

    

    void CeaseAction(Action ceasedAction)
    {
        UnityLoggingDelegate.LogIfTrue(LogEvents,UnityLoggingDelegate.LogType.General, ceasedAction.ToString() + " has ceased");
    }

     IEnumerator ExecuteAttackAction(WrappedAction wrappedAction, GameObject target)
    {
        //TODO
        yield return new WaitForEndOfFrame();
        wrappedAction.Done = true;
    }

    

    void ExecuteAgentDeath()
    {
        GameObject.Destroy(gameObject); //Should be last call of this meathod.
        UnityLoggingDelegate.LogIfTrue(LogEvents, UnityLoggingDelegate.LogType.General, "AI Agent: " + gameObject.transform.root.gameObject.name + " has been destroyed in scene");
    }

    void MoveTo(Transform target)
    {
        if (_MoveCoroutine != null)
        {
            _MoveCoroutine = StartCoroutine(MoveCoroutine(target));
        }
        else
        {
            UnityLoggingDelegate.LogIfTrue(LogEvents, UnityLoggingDelegate.LogType.Warning, "Tried to start a move corroutine that is already in progress");
        }

    }

    

    IEnumerator MoveCoroutine(Transform target)
    {
        yield return new WaitForEndOfFrame();
        _MoveCoroutine = null;
    }

    void StopActiveAction(WrappedAction wrappedAction)
    {
        wrappedAction.Done = true;
        StopCoroutine(wrappedAction.ActionCoroutineProp);
    }
    void StopActiveCoroutine(Coroutine coroutine)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
    }

    private class WrappedAction
    {
        private Coroutine _ActionCoroutine;
        private Action _Action;
        private GameObject _Target;
        public bool Done;
        public WrappedAction(Coroutine coroutine, Action action, GameObject target, bool coroutineDone)
        {
            _ActionCoroutine = coroutine;
            _Action = action;
            _Target = target;
            Done = coroutineDone;
        }

        public Coroutine ActionCoroutineProp
        {
            get{return _ActionCoroutine;}
            set{_ActionCoroutine = value;}
        }

        public Action ActionProp
        {
            get{return _Action;}
            set{_Action = value;}
        }

        public GameObject TargetProp
        {
            get{return _Target;}
            set{_Target = value;}
        }
    }
    

    
}
