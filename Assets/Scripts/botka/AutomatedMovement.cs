using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomatedMovement : MonoBehaviour
{
    public enum Status
    {
        Moving,Stopped,Halted, Stuck, Inactive
    }
    [Range(0.1f,10f)]public float VelocityX;
    [Range(0.1f,10f)]public float VelocityY;
    [Range(0.1f,10f)]public float JumpPower;
    [Range(0.1f,10f)]public float FallSpeed;
    [Range(0.1f,10f)]public float Gravity;

    


    [Range(0.2f, 100f)]public float DistanceErrorMargin;
    public Rigidbody2D RigidBody2d;

    public bool Lock;
    public bool Moving;

    [Header("DEBUG")]
    [HideInInspector]public GameObject Target;
    public Transform TargetLocation;
    public Status EntityStatus;
    private Status EntityState;
    private Status _PausedState;
    
    private Coroutine _PauseTimeout;

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
        }
        EntityState = Status.Inactive;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Target != null)
        {
            TargetLocation = Target.transform;
        }
        EntityStatus = EntityState;
        if (Lock)
        {
            StopMoving();
        }
        else
        {
            if (RigidBody2d.velocity.y > 0)
            {
                RigidBody2d.gravityScale = Gravity * FallSpeed;
            }
            switch(EntityState)
            {
                case Status.Inactive:
                    break;
                case Status.Halted:
                    if (_PauseTimeout == null)
                    {
                        EntityState = _PausedState;
                    }
                    break;
                case Status.Moving:
                    Moving = true;
                    if (isAtLocation(Target.transform))
                    {
                        StopMoving();
                        break;
                    }
                    Vector3 vec = Target.transform.position - transform.position;
                    vec = vec.normalized;
                    
                    if (RigidBody2d == null || !RigidBody2d.simulated) // if null then it will not test the second condition
                    {
                        vec = new Vector3(vec.x * VelocityX, vec.y * VelocityY, vec.z);
                        gameObject.transform.position += vec;
                    } 
                    else
                    {
                        //transform.Translate((Vector2)transform.position +  new Vector2(vec.x, vec.y) * Time.deltaTime);
                        RigidBody2d.MovePosition((Vector2)transform.position +  new Vector2(vec.x * VelocityX, vec.y * VelocityY)  * Time.fixedDeltaTime);
                    }
                    break;
                case Status.Stopped:
                    break;
                case Status.Stuck:
                    break;
                default:
                    break;
            }
        }
    }

    private void Jump()
    {
        RigidBody2d.velocity = new Vector2(RigidBody2d.velocity.x, 0);
        RigidBody2d.AddForce(new Vector2(0, 5f), ForceMode2D.Impulse);
    }
    public void StartMoving()
    {
        EntityState = Status.Moving;
        RigidBody2d.velocity = new Vector2(VelocityX, VelocityY);
        
    }

    public void StopMoving()
    {
        EntityState = Status.Stopped;
        RigidBody2d.velocity = new Vector2(0f, 0f);
        
    }

    public void PauseMovement(float seconds)
    {
        _PausedState = EntityState;
        EntityState = Status.Halted;
        if (_PauseTimeout != null)
        {
            StopCoroutine(_PauseTimeout);
            _PauseTimeout = null;
        }
        _PauseTimeout = StartCoroutine(TimeoutCoroutine(seconds));
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
        Debug.Log( Mathf.Abs(Vector2.Distance(new Vector2(loca.position.x, loca.position.y), new Vector2(gameObject.transform.position.x, gameObject.transform.position.y))) <= distanceMargin );
        
        return loca != null ? 
            Mathf.Abs(Vector2.Distance(new Vector2(loca.position.x, loca.position.y), new Vector2(gameObject.transform.position.x, gameObject.transform.position.y))) <= distanceMargin 
                : false;
    }
   
    private IEnumerator TimeoutCoroutine(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        yield return new WaitForEndOfFrame();
    }
}
