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
    [Range(0.1f,10f)]public float JumpPower;
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
    
    public bool Moving;

    [Header("DEBUG")]
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
    private bool _Grounded;

    private GameObject _StandingOn;
    public GameObject[] BlockingObjects;
    void Awake() 
    {
        _PauseTimeout = null;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (RigidBody2d == null)
        {
            RigidBody2d = GetComponentInChildren<Rigidbody2D>();
            RigidBody2d.gravityScale = Gravity;
        }
        EntityStatus = Status.Inactive;
        _EntityBounds = GetComponentInChildren<SpriteRenderer>().bounds;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Target != null)
        {
            TargetLocation = Target.transform;
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
                RigidBody2d.gravityScale = Gravity * FallSpeed;
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
                            Debug.Log(nX);
                            RaycastHit2D nHit = Physics2D.Raycast(
                                new Vector2(nX, 100f), 
                                    Vector2.down);
                            if (nHit.collider != null)
                            {
                                RigidBody2d.MovePosition(new Vector3(nHit.point.x,nHit.point.y + _EntityBounds.size.y / 2, transform.position.z));
                                break;
                                
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
                        }
                    }
                    if (Target != null)
                    {
                        Vector3 vec = Target.transform.position - transform.position;
                        vec = vec.normalized;
                        if (RigidBody2d == null || !RigidBody2d.simulated) // if null then it will not test the second condition
                        {
                            vec = new Vector3(vec.x * VelocityX, vec.y * VelocityY, vec.z);
                            gameObject.transform.position += vec;
                        } 
                        else
                        {
                            transform.Translate(new Vector2(vec.x * -1 * VelocityX * Time.deltaTime,0));
                            //RigidBody2d.AddForce(new Vector2(vec.x * -1 * VelocityX * Time.deltaTime,0));
                            //RigidBody2d.MovePosition((Vector2)transform.position +  new Vector2(vec.x * VelocityX, vec.y * VelocityY)  * Time.fixedDeltaTime);
                        }
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
        if (Jumpable)
        {
            RigidBody2d.velocity = new Vector2(RigidBody2d.velocity.x, 0);
            RigidBody2d.AddForce(new Vector2(0, 5f), ForceMode2D.Impulse);
            if (_JumpCoroutine == null)
            {
                _Jumping = true;
                _JumpCoroutine = StartCoroutine(JumpCoroutine());
            }
        }
        else
        {
            UnityLoggingDelegate.LogIfTrue(LogEvents, UnityLoggingDelegate.LogType.Warning, "Attempted to jump -- Can not jump as entity is listed as not jumpuable");
        }
    }

    [ContextMenu("Start movement")]
    public void StartMoving()
    {
        EntityStatus = Status.Moving;
        RigidBody2d.velocity = new Vector2(VelocityX, VelocityY);
    }

    [ContextMenu("Stop Movement")]
    public void StopMoving()
    {
        EntityStatus = Status.Stopped;
        RigidBody2d.velocity = new Vector2(0f, 0f);
    }

    [ContextMenu("Pause Movement")]
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

    public bool IsAtTarget()
    {
        return Target != null ? isAtLocation(Target.transform) : false;
    }

    public bool IsAtTarget(float margin)
    {
        return Target != null ? isAtLocation(Target.transform, margin) : false;
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

    private IEnumerator JumpCoroutine()
    {
        RigidBody2d.velocity = new Vector2(RigidBody2d.velocity.x, 0);
        RigidBody2d.AddForce(new Vector2(0, 5f), ForceMode2D.Impulse);
        yield return new WaitForSecondsRealtime(JumpLifOffTime);
        //enter haning code
        yield return new WaitForSecondsRealtime(HangTime);
        while (!_Grounded)
        {
            RigidBody2d.gravityScale = Gravity * FallSpeed;
        }
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
        }
        
    }

    private void OnCollisionStay2D(Collision2D other) 
    {
        if (other.gameObject.tag == "Ground")
        {
            _Grounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D other) 
    {
        if (other.gameObject.tag == "Ground")
        {
            _Grounded = false;
            
        }
    }

    
}
