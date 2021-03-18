using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * @Author Jake Botka
 * 
 */
 [DisallowMultipleComponent]
public class AgentBehavior : MonoBehaviour
{
    
    public const Action DefaultAction = Action.Halt;
    public const BehavioralAggression DefaultAggression = BehavioralAggression.Passive;
    public Action[] AllActions
    {  get{return new Action[] {Action.Move, Action.Attack, Action.Halt, Action.Patrol, Action.Persue};}
        set{}
    }
    
    public AutomatedMovement AutomatedMovementScript;
    public AgentState AgentStateScript;
    public Action StartingAction;
    public BehavioralAggression StartingAggression;

    [Header("Attack Options")]
    [InspectorName("Enabled")]public bool AttackEnabled;
    public float DistanceToAttack;
    public AttackScriptableObject[] SupportedAttacks;
    

    [Header("Patrol options")]
    [InspectorName("Enabled")]public bool PatrolEnabled;
    [Range(0f,10f)]public float PauseAtPoint;
    public Transform[] PatrolPoints;
    [Range(0f,10f)]public float VelocityX;
    [Range(0f, 10f)]public float VelocityY;
    [Range(0f,10f)] public float StoppingDistance;

    public GameObject PatrolPointsParent
    {
        get{return PatrolPointsParent;}
        set
        {
            if (value != null)
            {
                Transform trans = value.transform;
                PatrolPoints = new Transform[trans.childCount];
                for (int i= 0; i < trans.childCount; i++)
                {
                    PatrolPoints[i] = trans.GetChild(i);
                }
            }
        }
    }

    


    public enum Action
    {
        Null,Patrol, Attack, Persue, Halt, Move
    }
    public enum BehavioralAggression 
    {
        Null,Passive, Moderate, Agressive
    } 
    
    
    [Header("Target Information - DO NOT SET")]
    public GameObject Target;
    public Transform TargetTransform;

    [Header("Adittional Options")]
    public bool LogEvents;
    private float _DistanceToTarget;

     private List<int> _PendingRemovals = new List<int>(0);
   
    private List<WrappedAction> _ActiveActionRoutines;
    
    [Header("DEBUG")]
    public Action[] ActiveActionsBeingPerformed;
   

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
            Target = Target.gameObject;
        }
        else
        {
            Target = GameObject.FindGameObjectWithTag("Player");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        ExecuteAction(StartingAction);
        ApplyAggression(StartingAggression);
        AutomatedMovementScript.VelocityX = VelocityX;
        AutomatedMovementScript.VelocityY = VelocityY;
    }

   
    void FixedUpdate()
    {
        AutomatedMovementScript.Lock =  AgentStateScript.PosLock == true ? true : AutomatedMovementScript.Lock;
        //Checks which active coroutine are finished. This is schronized as removal in a cororutine is not possible due to lack of synchronization in list objects.
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
            UnityLoggingDelegate.LogIfTrue(LogEvents, UnityLoggingDelegate.LogType.General, "Completed action removed: " + _ActiveActionRoutines[i].ActionProp);
            _ActiveActionRoutines.RemoveAt(i); 
        }

        if (_PendingRemovals.Count > 0)
        {
            ActiveActionsBeingPerformed = _ActiveActionRoutines.ConvertAll<Action>(e => e.ActionProp).ToArray();
            _PendingRemovals.Clear();
        }
        
        HandleDistanceToTarget();
        HandleIfInView();
    }

    [ContextMenu("Start Patrol")]
    public void StartPatrol()
    {
        ExecuteAction(Action.Patrol);
    }

    [ContextMenu("Attack Target")]
    public void StartAttack()
    {
        ExecuteAction(Action.Attack ,Target);
    }

    [ContextMenu("Persue Target")]
    public void StartPersue()
    {
        ExecuteAction(Action.Persue, Target, true);
    }

    [ContextMenu("Halt")]
    public void Halt()
    {
        ExecuteAction(Action.Halt);
    }
    void HandleDistanceToTarget()
    {
        if (Target != null)
        {
            _DistanceToTarget = Vector3.Distance(Target.transform.position, transform.position);
            //Basic checking
            
            if (!HasActiveAction(Action.Attack) && _DistanceToTarget   <= DistanceToAttack + AgentStateScript.AgentBounds.size.x)
            {
                ExecuteAction(Action.Attack, Target);
            }
        }
        else
        {
            _DistanceToTarget = 0f;
        }
    }

    void HandleIfInView()
    {
        if (AgentStateScript.TargetInView(Target))
        {
            if (!HasActiveAction(Action.Persue) && !HasActiveAction(Action.Attack))
            {
                ExecuteAction(Action.Persue, Target);
            }
        }
    }

    void ApplyAggression(BehavioralAggression aggression)
    {

    }

    bool HasActiveAction(Action action)
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
    

    public void ExecuteAction(Action action)
    {
        if (StartingAction == Action.Attack || StartingAction == Action.Persue)
        {
            ExecuteAction(action,Target, false);   
        }
        else
        {
            ExecuteAction(action, null, false);
        }
    }

    public void ExecuteAction(Action action, GameObject target, bool ovverrideAllActions)
    {
        ExecuteAction(action,target, ovverrideAllActions, false);
    }

    public void ExecuteAction(Action action, GameObject target, bool ovverrideAllActions, bool overrideExistingAction)
    {
        if (_ActiveActionRoutines.Count > 0) 
        {
            if (ovverrideAllActions)
            {
                StopAllActiveActions();
                Halt();
            }
            else
            {
                if (overrideExistingAction == true)
                {
                    StopActiveAction(action);
                }
            }
            
            if (!HasActiveAction(action))
            {
                ExecuteAction(action,target);
            }
            else
            {
                UnityLoggingDelegate.LogIfTrue(LogEvents, UnityLoggingDelegate.LogType.Warning, 
                "Attemted to start an action that already exists and the ovveride value is set to false. This call is ignored");
            }
        }
        else
        {
            ExecuteAction(action, target);
        }
        
    }

    public void ExecuteAction(Action action, GameObject targetOfAction)
    {
        Coroutine c = null;
        WrappedAction w = w = new WrappedAction(null,action, targetOfAction, false);
        AutomatedMovementScript.DisableJumping = false;
        switch(action)
        {
            
            case Action.Attack:
                c = StartCoroutine(ExecuteAttackAction(w,targetOfAction));
                break;
            case Action.Patrol:
                if (!HasActiveAction(Action.Patrol))
                {
                    if (!HasActiveAction(Action.Move))
                    {
                        WrappedAction w1 = _ActiveActionRoutines.Find((e) => e.ActionProp == Action.Move);
                        if (w1 != null)
                        {
                            StopActiveAction(w1);
                        }
                    }
                    else
                    {
                        Debug.LogError("Attemting to Start patrol action when already patrolling");
                    }
                }
                c = StartCoroutine(ExecutePatrolAction(w,PatrolPoints));
                break;
            case Action.Halt:
                AutomatedMovementScript.StopMoving();
                StopAllActiveActions();
                w = null;
                break;
            case Action.Persue:
                if (!HasActiveAction(Action.Attack))
                {
                    AutomatedMovementScript.Targets.Clear();
                    c = StartCoroutine(ExecutePersueAction(w,Target));
                }
                break;
            case Action.Move:
                c = StartCoroutine(ExecuteMoveAction(w,targetOfAction.transform));
                break;
            default:
                break;
        }

        UnityLoggingDelegate.LogIfTrue(LogEvents, UnityLoggingDelegate.LogType.General, "Action initated: " + action.ToString() 
            + " on target: " + (targetOfAction != null ? targetOfAction.name : "Null"));
        if (w != null)
        {
            w.ActionCoroutineProp = c;
            _ActiveActionRoutines.Add(w);
            ActiveActionsBeingPerformed = _ActiveActionRoutines.ConvertAll<Action>(e => e.ActionProp).ToArray();
        }
        
    }

    private IEnumerator ExecuteAttackAction(WrappedAction wrappedAction, GameObject target)
    {
        //TODO
        yield return new WaitUntil(() => false); // replace condition testing when the attack is finished
        yield return new WaitForEndOfFrame();
        wrappedAction.Done = true;
    }
    private bool _BreakOfPatrol;
    private IEnumerator ExecutePatrolAction(WrappedAction action, Transform[] patrolPoints)
    {
        UnityLoggingDelegate.LogIfTrue(LogEvents, UnityLoggingDelegate.LogType.General, "AI Agent: " + gameObject.name + " Has begun patrolling");
        while(true)
        {
            
            if (patrolPoints != null )
            {
                AutomatedMovementScript.VelocityX = VelocityX;
                AutomatedMovementScript.VelocityY = VelocityY;
                AutomatedMovementScript.DisableJumping = true;
                AutomatedMovementScript.StartMoving();
                foreach (Transform transform in patrolPoints)
                {
                    
                    AutomatedMovementScript.Targets.Add(transform.gameObject);
                    yield return new WaitUntil(() => AutomatedMovementScript.IsAtTarget(transform.gameObject, StoppingDistance) || _BreakOfPatrol);
                    AutomatedMovementScript.Targets.Remove(transform.gameObject);
                    if (!_BreakOfPatrol)
                    {
                        yield return new WaitForSeconds(PauseAtPoint);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else
            {
                Debug.LogError("Attemting to patrol but no patrol points are provided");
            }
            if (_BreakOfPatrol)
            {
                AutomatedMovementScript.DisableJumping = false;
                break;
            }
        }
        
        AutomatedMovementScript.StopMoving();
        yield return new WaitForEndOfFrame();
        action.Done = true;
        
    }

    private IEnumerator ExecutePersueAction(WrappedAction wrappedAction, GameObject target)
    {
        if (AutomatedMovementScript.Targets.Find((e) => e.name == target.name) == null)
        {
            AutomatedMovementScript.Targets.Add(target);
        }
        AutomatedMovementScript.VelocityX = VelocityX;
        AutomatedMovementScript.VelocityY = VelocityY;
        AutomatedMovementScript.StartMoving();
        
        yield return new WaitUntil(() => AutomatedMovementScript.IsAtTarget(target.gameObject, StoppingDistance +  GetComponentInChildren<SpriteRenderer>().bounds.size.x)); // replace condition testing when the attack is finished
        yield return new WaitForEndOfFrame();
        wrappedAction.Done = true;
        
    }


   

    public void MoveTo(Transform target, bool overrideExistingAction)
    {
        if (overrideExistingAction)
        {
            if (!HasActiveAction(Action.Move))
            {

            }
        }
        if (!HasActiveAction(Action.Move))
        {
            Coroutine c = null;
            WrappedAction w = w = new WrappedAction(null,Action.Move, target.gameObject, false);
            c = StartCoroutine(ExecuteMoveAction(w,target));
            w.ActionCoroutineProp = c;
            _ActiveActionRoutines.Add(w);
        }
        else
        {
            UnityLoggingDelegate.LogIfTrue(LogEvents, UnityLoggingDelegate.LogType.Warning, "Tried to start a move corroutine that is already in progress");
        }

    }

    

    IEnumerator ExecuteMoveAction(WrappedAction wrappedAction, Transform target)
    {
        yield return new WaitForEndOfFrame();
        wrappedAction.Done = true;
    }

    public void StopAllActiveActions()
    {
        StopActiveActions(Action.Attack,Action.Halt,Action.Move,Action.Patrol, Action.Persue);
    }

    //params just takes all arguements and condence them in an array. so instead of inserting an array you insert multiple arguments
    public void StopActiveActions( params Action[] actions)
    {
        if (actions != null)
        {
            foreach(Action action in actions)
            {
                StopActiveAction(action);
            }
        }
    }
    void StopActiveAction(Action action)
    {
        WrappedAction wrappedAction = _ActiveActionRoutines.Find((e) => e.ActionProp == action);
        if (wrappedAction != null)
        {
            wrappedAction.Done = true;
            if (wrappedAction.ActionCoroutineProp != null)
            {
                StopCoroutine(wrappedAction.ActionCoroutineProp);
            }
        }
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

    private void OnBehaviorChanged(bool halt)
    {

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
