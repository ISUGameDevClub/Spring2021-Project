using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[DisallowMultipleComponent]
public class AutomatedMovement : MonoBehaviour
{
    public enum Status
    {
        Moving,Stopped,Halted, Stuck, Inactive
    }
    [Header("General")]
    [Range(0.1f,10f)]public float VelocityX;
    [Range(0.1f,10f)]public float VelocityY;
    [Range(0.5f, 50f)] public float MinJumpingPower;
    [Range(0.5f, 50f)] public float MaxJumpingPower;
    [Range(1f,50f)]public float JumpPower;
    [Range(0.1f,10f)]public float FallSpeed;
    [Range(0.1f,10f)]public float Gravity;

    [Header("Jump Options")]
    public bool Jumpable;
    [Range(0f,8f)]public float JumpLifOffTime;
    [Range(0f,8f)]public float HangTime;

    public bool LogEvents;

    [Range(0.2f, 100f)]public float DistanceErrorMargin;
    [Range(0f,10f)]public float PathBlockingDistance;
    public Rigidbody2D RigidBody2d;

    public bool Lock;
    
    [HideInInspector]public bool Moving;

    [Header("DEBUG")]
    public List<GameObject> Targets;
    public GameObject Target;
    public bool Blocked;
    public Transform TargetLocation;
    public Status EntityStatus;
    
    [HideInInspector]public bool DisableJumping;
    
    private Coroutine _PauseTimeout;
    private Coroutine _JumpCoroutine;
    private Bounds _EntityBounds;
    private bool _LocalLock;
    private bool _LocalLockBindedToLock;
    private bool _Jumping;
    public bool _Grounded;
    private Coroutine _JumpTimeout;

    private GameObject _StandingOn;
    public GameObject[] BlockingObjects;
    void Awake() 
    {
        _JumpTimeout = null;
        _PauseTimeout = null;
        Targets = new List<GameObject>();
        RigidBody2d.Sleep();
    }
    // Start is called before the first frame update
    void Start()
    {
        RigidBody2d.WakeUp();
        
        if (RigidBody2d == null)
        {
            RigidBody2d = GetComponentInChildren<Rigidbody2D>();
            RigidBody2d.gravityScale = Gravity;
        }
        EntityStatus = Status.Inactive;
        _EntityBounds = GetComponentInChildren<SpriteRenderer>().bounds;
        _Grounded = RigidBody2d.IsTouching(GameObject.FindGameObjectWithTag("Ground").GetComponentInChildren<Collider2D>());
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!_Grounded)
        {
            if ( _SimFallCoroutine == null) 
            {
                _Timeout = StartCoroutine(ForceDownTimeoutCoroutine());
                _SimFallCoroutine = StartCoroutine(ForceDownCoroutine());
            }
        }
        if (Target != null)
        {
            TargetLocation = Target.transform;
        }
        
        if (_Grounded)
        {
            if (RigidBody2d.gravityScale != Gravity)
            {
                RigidBody2d.gravityScale = Gravity;
            }
        }
        else
        {
            float temp = Gravity * FallSpeed;
            if (RigidBody2d.gravityScale != temp)
            {
                RigidBody2d.gravityScale = temp;

            }
        }
        if (Lock || _LocalLock)
        {
            if (Moving)
            {
                PauseMovement(-1f);
            }
        }
        else
        {
            if (RigidBody2d.velocity.y < 0)
            {
               // RigidBody2d.gravityScale = Gravity * FallSpeed;
            }
            
            if (Targets.Count > 0 && Targets[0] != null)
            {
                Target = Targets[0];
                EntityStatus = Status.Moving;
            }
            else
            {
                Target = null;
            }
            switch(EntityStatus)
            {
                case Status.Inactive:
                    break;
                case Status.Halted:
                    if (_PauseTimeout == null)
                    {
                        EntityStatus = Status.Stopped;
                        
                    }
                    break;
                case Status.Moving:
                    Moving = true;
                    if (Jumpable && !DisableJumping)
                    {
                        RaycastHit2D hit = CheckBlockingPath(); 
                        Vector2 point = hit.point;
                        Vector2 v = point - (Vector2)transform.position;
                        if (hit.collider != null)
                        {
                            Tilemap tilemap = hit.collider.gameObject.GetComponentInChildren<Tilemap>();
                            float nX = point.x - transform.position.x <= 0f ? transform.position.x + (point.x - transform.position.x) - _EntityBounds.size.x / 2
                            : transform.position.x + (point.x - transform.position.x) + _EntityBounds.size.x / 2;
                            //Debug.Log(nX);
                            RaycastHit2D nHit = Physics2D.Raycast(
                                new Vector2(nX, 100f), 
                                    Vector2.down);
                            if (nHit.collider != null)
                            {
                                RigidBody2d.MovePosition(new Vector3(nHit.point.x,nHit.point.y + _EntityBounds.size.y / 2, transform.position.z));
                                break; //remove when jumping  is finished
                                float modPower = RigidBody2d.velocity.x * Vector3.Distance(transform.position, point) * 3;
                                modPower = modPower > MaxJumpingPower || modPower < MinJumpingPower ? 
                                        (JumpPower > MaxJumpingPower || JumpPower < MinJumpingPower 
                                            ? MaxJumpingPower : JumpPower) : modPower;
                                Debug.Log(modPower);
                                if (!IsStationary())
                                {
                                    Jump(modPower,0f);
                                    break;
                                }
                                else
                                {
                                    Jump(modPower, transform.position.z == 0f ? 10f : -10f);
                                } 
                            }
                        }
                    }

                    if (Targets.Count > 0)
                    {
                        Target = Targets[0];
                    }
                    else
                    {
                        Target = null;
                    }

                    if (Target != null)
                    {
                        Vector3 vec = Target.transform.position - transform.position;
                        
                        vec = vec.normalized;
                        
                        RotateToTarget(Target.transform);
                        
                        vec = new Vector3(vec.x * VelocityX * Time.deltaTime, RigidBody2d.velocity.y *  Time.deltaTime, vec.z);
                        
                        
                        //gameObject.transform.position += vec;
                        RigidBody2d.MovePosition(transform.position + vec);
                         
                    }
                    else
                    {
                        Moving = false;
                        EntityStatus = Status.Halted;
                    }
                    break;
                case Status.Stopped:
                    if (Moving)
                    {
                        StopMoving();
                    }
                    break;
                case Status.Stuck:
                    break;
                default:
                    break;
            }
        }

        if (IsAtTarget())
        {
            Targets.Remove(Target);
            Target = null;
        }
    }

    private List<GameObject> _TempList = new List<GameObject>(0);
    private RaycastHit2D CheckBlockingPath()
    {
        //transform.right is alwars facing front as the rotation of the Z axis determiens which way the entity is looking
        return Physics2D.BoxCast(new Vector2(transform.position.x, transform.position.y + (_EntityBounds.size.y * 0.2f)), _EntityBounds.size, 0f, 
            transform.right, PathBlockingDistance, LayerMask.GetMask("Default", "Blockable"));
        
    }

    [ContextMenu("Jump")]
    public void Jump()
    {
        Jump(JumpPower);
    }

    public void Jump(float jumpingPower)
    {
        Jump(JumpPower, 0f);
    }

    public void Jump(float jumpingPower, float XVecPowerHanging)
    {
        if (Jumpable)
        {
            //RigidBody2d.velocity = new Vector2(RigidBody2d.velocity.x, 0);
            //RigidBody2d.AddForce(new Vector2(0, 5f), ForceMode2D.Impulse);
            if (_JumpCoroutine == null && _Grounded && !_Jumping)
            {
                _Jumping = true;
                _JumpCoroutine = StartCoroutine(JumpCoroutine(jumpingPower, XVecPowerHanging));
                
            }
        }
        else
        {
            UnityLoggingDelegate.LogIfTrue(LogEvents, UnityLoggingDelegate.LogType.Warning, "Attempted to jump -- Can not jump as entity is listed as not jumpuable");
        }
    }

    public bool IsStationary()
    {
        return RigidBody2d.velocity.x <= 0.1f && RigidBody2d.velocity.x >= -0.1f;
    }

    public void Turn(float degrees)
    {
        float x = transform.localEulerAngles.z + degrees;
        float overunOffset = 360f - x;
        float setDegree = overunOffset >= 0f ? overunOffset * -1 : overunOffset; //if over 360 degres then transform its relatively to zero. 360 degree in euler is the same at 0 degrees euler.
        transform.localEulerAngles = new Vector3(0,0,setDegree);
    }

    [ContextMenu("Turn Around")]
    public void TurnOppositeDir() 
    {
        Turn(180f);
    }

    [ContextMenu("Start movement")]
    public void StartMoving()
    {
        UnityLoggingDelegate.LogIfTrue(LogEvents,UnityLoggingDelegate.LogType.General, ("AI Started to move"));
        EntityStatus = Status.Moving;
        RigidBody2d.velocity = new Vector2(VelocityX, RigidBody2d.velocity.y);
    }

    private Coroutine _DragCoroutine;
    [ContextMenu("Stop Movement")]
    public void StopMoving()
    {
        if (EntityStatus != Status.Stopped)
        {
            UnityLoggingDelegate.LogIfTrue(LogEvents,UnityLoggingDelegate.LogType.General, ("AI Stopped moving"));
            EntityStatus = Status.Stopped;
            RigidBody2d.velocity = new Vector2(0f, RigidBody2d.velocity.y);
            RigidBody2d.angularVelocity = 0;
            //if (_DragCoroutine == null) _DragCoroutine = StartCoroutine(DragCoroutine());
        }
    }

    private IEnumerator DragCoroutine() 
    {
        float dragMax = 20f;
        float animStep = 30f;
        float perAnimStep = dragMax / animStep;
        float temp = RigidBody2d.drag;
        for (var x = 0; x < animStep; VelocityX++)
        {
            RigidBody2d.drag = RigidBody2d.drag + perAnimStep;
            yield return new WaitForSeconds(2f / animStep);
        }
        
        RigidBody2d.drag = temp;
        _DragCoroutine = null;
    }

    public void PauseMovement(float seconds)
    {
        // seconds < 0f means indenfinite pause. Lift the pause by reseting the Lock variable
        UnityLoggingDelegate.LogIfTrue(LogEvents, UnityLoggingDelegate.LogType.General,
                 "Movement was paused for: " + (seconds > 0f ? seconds.ToString() : "Indefinite amount of") +  " Seconds.");
        if (seconds > 0f)
        {
            _LocalLock = true;
            EntityStatus = Status.Halted;
            
            if (_PauseTimeout != null)
            {
                StopCoroutine(_PauseTimeout);
                _PauseTimeout = null;
            }
            _PauseTimeout = StartCoroutine(TimeoutCoroutine(seconds));
            
        } 
        Moving = false;
    }

    public void RotateToTarget(Transform target)
    {
        Vector3 vec = Target.transform.position - transform.position;
        vec = vec.normalized;
        if (vec.x > 0) 
        {
            if(transform.localEulerAngles.z >= 180f)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0f);
            }
        }
        else
        {
            if(transform.localEulerAngles.z < 180f)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 180f);
            }
        }
    }

    [ContextMenu("Look at Target")]
    public void RotateToTarget()
    {
        RotateToTarget(Target.transform);
    }

    public bool IsAtTarget()
    {
        return Target != null ? isAtLocation(Target.transform) : false;
    }

    public bool IsAtTarget(float margin)
    {
        return Target != null ? isAtLocation(Target.transform, margin) : false;
    }

     public bool IsAtTarget(GameObject target, float margin)
    {
        return target != null ? isAtLocation(target.transform, margin) : false;
    }

    public bool isAtLocation(Transform loca)
    {
        return isAtLocation(loca,DistanceErrorMargin);
    }

    public bool isAtLocation(Transform loca, float distanceMargin)
    {
        return loca != null ? 
            Mathf.Abs(Vector2.Distance(new Vector2(loca.position.x, loca.position.y), new Vector2(gameObject.transform.position.x, gameObject.transform.position.y))) <= distanceMargin 
                : false;
    }
   
    private IEnumerator TimeoutCoroutine(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        yield return new WaitForEndOfFrame();
        _PauseTimeout = null;
        _LocalLock = false;
        
    }

    private IEnumerator JumpCoroutine(float JumpingPower, float hangingMove)
    {
        Debug.Log("Jumping)");
        RigidBody2d.velocity = new Vector2(RigidBody2d.velocity.x, RigidBody2d.velocity.y);
        RigidBody2d.AddForce(new Vector2(0, JumpingPower), ForceMode2D.Impulse);
        yield return new WaitForSecondsRealtime(JumpLifOffTime);
        //enter haning code
        yield return new WaitForSecondsRealtime(HangTime);
        if (hangingMove > 0f)
        {
            RigidBody2d.AddForce(new Vector2(hangingMove, 0), ForceMode2D.Impulse);
        }
        RigidBody2d.gravityScale = Gravity * FallSpeed;
        yield return new WaitForEndOfFrame();
        _Jumping = false;
        _JumpCoroutine = null;
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.tag == "Ground")
        {
            _Grounded = true;
            _StandingOn = other.gameObject;
            RigidBody2d.velocity = new Vector2(RigidBody2d.velocity.x, 0);
        }
        
    }

    private void OnCollisionStay2D(Collision2D other) 
    {
        if (other.gameObject.tag == "Ground")
        {
            _Grounded = true;
            
        }
    }
    
    private Coroutine _SimFallCoroutine;
    private Coroutine _Timeout;
    private void OnCollisionExit2D(Collision2D other) 
    {
        if (other.gameObject.tag == "Ground")
        {
            _Grounded = false;
            
        }
    }

    private IEnumerator ForceDownTimeoutCoroutine()
    {
        yield return new WaitForSeconds(5f);
        _Timeout = null;
        
    }

    private IEnumerator ForceDownCoroutine()
    {
        while (true && _Grounded == false && _Timeout != null)
        {
            Debug.Log("AAA");
            RigidBody2d.velocity = new Vector2(RigidBody2d.velocity.x, RigidBody2d.velocity.y + -1f);
            yield return new WaitForSeconds(0.25f);
        }
        if (_Timeout != null) StopCoroutine(_Timeout);
        _SimFallCoroutine = null;
        yield return new WaitForFixedUpdate();
    }
    

    
}



/*
                            Tilemap tilemap = hit.collider.gameObject.GetComponentInChildren<Tilemap>();
                            Vector3Int intV = tilemap.LocalToCell(tilemap.WorldToLocal(new Vector3(point.x,point.y,0)));
                            TileBase[] tiles = tilemap.GetTilesBlock(tilemap.cellBounds);
                            while (tilemap.HasTile(intV))
                            {
                                intV.y += tilemap.cellBounds.size.y;
                            }
                            if (tilemap != null)
                            {
                                Bounds b = tilemap.GetBoundsLocal(intV);
                                float x = v.x <= 0f ? b.min.x : b.max.x + _EntityBounds.size.x;
                                float y =  v.x <= 0f ? b.max.y : b.max.y +  _EntityBounds.size.y;
                                if (b != null)
                                {
                                    Debug.Log(b.min +"," + b.max);
                                    Debug.Log(new Vector3(x,y, transform.position.z));
                                    RigidBody2d.MovePosition(new Vector3(x,y, transform.position.z));
                                    Lock = true;
                                    break;
                                }
                            }
                            */