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
    
    [Header("Vision Options")]
    [Range(0f,180f)] public float POVForward;
    [Range(0f,180f)] public float POVBackward;
    
    [Range(0f,1000f)] public float VisionDistance;

    [Header("Additional Options")]
    public bool Lock;
    public bool PosLock;

    
    public bool FixedPathing;
    public bool VariablePathing;
    
    [Header("DEBUG")]
    public Transform AgentTransform;
    public Bounds AgentBounds;
    public Vector3 ForwardDir;
    public Transform ViewDirection;

    public EntityStatus EntityState;
   
    public GameObject[] ObjectsInView;
    public GameObject[] ObjectsSensedBehind;
    public bool LogEvents;

    private Transform _CopyTransform;
    public Vector2 _BoxCastSizeFront;
    private Vector2 _BoxCastSizeBack;
    private float _LastRecordedPOVF;
    private float _LastRecordedPOVB;
    private float _LastRecordedDistance;

    private Agent _AgentScript;

    public Vector2 Position 
    {
        get{return transform.position;}
    }

    public bool CanMove
    {
        get{return Lock == true || PosLock == true;}
        set{CanMove = value;}
    }
    public bool CanAttack
    {
        set{CanAttack = value;}
    }


    void Awake()
    {
        EntityState = EntityStatus.NotSpawned;
        _BoxCastSizeFront = Vector2.negativeInfinity;
        _BoxCastSizeBack = Vector2.negativeInfinity;
    }
    
    void Start()
    {
        _AgentScript = GetComponent<Agent>();
        if (_AgentScript == null)
        {
            _AgentScript = GetComponentInParent<Agent>();
        }
        AgentBounds = GetComponentInChildren<SpriteRenderer>().bounds;
        _CopyTransform = transform.childCount > 0 ? transform.GetChild(0).transform : null;
        EntityState = EntityStatus.Idle;
        _LastRecordedDistance = VisionDistance;
        OnChangedPOVF();
        OnChangedPOVB();
    }

    List<GameObject> FilteredHits = new List<GameObject>(0);
    void FixedUpdate()
    {
        AgentTransform = transform;
        ForwardDir = transform.right;
        _CopyTransform.transform.rotation = transform.rotation;
        if (Lock)
        {
            PosLock = Lock;
        }
        if (_LastRecordedDistance != VisionDistance)
        {
            OnChangedVisionDistance();
        }
        if (_LastRecordedPOVF != POVForward)
        {
            OnChangedPOVF();
        }
        if (_LastRecordedPOVB != POVBackward)
        {
            OnChangedPOVB();
        }
        RaycastHit2D[] hits = CheckBoxCastHitsForward();
        foreach(RaycastHit2D hit in hits)
        {
            
            float angle = Vector3.Angle(_AgentScript.MainTarget.transform.position - transform.position, ForwardDir);
            if (angle >= 0f && angle <= POVForward / 2)
            {
                FilteredHits.Add(hit.collider.gameObject.transform.root.gameObject);
            }
        }
        ObjectsInView = FilteredHits.ToArray();
        FilteredHits.Clear();

        hits = CheckBoxCastHitsBackward();
        
        
        foreach(RaycastHit2D hit in hits)
        {
            float angle = Vector3.Angle(_AgentScript.MainTarget.transform.position - transform.position, ForwardDir);
            if (angle >= 0f && angle <= POVBackward / 2)
            {
                FilteredHits.Add(hit.collider.gameObject.transform.root.gameObject);
            }
        }

        ObjectsSensedBehind = FilteredHits.ToArray();
        FilteredHits.Clear();
        
        DrawVisionCone();
        

    }

    private void DrawVisionCone()
    {
        Vector3 Reset = new Vector3(_CopyTransform.eulerAngles.x,_CopyTransform.eulerAngles.y, _CopyTransform.eulerAngles.z);
       Debug.DrawLine(transform.position, transform.position + ForwardDir * VisionDistance, Color.green, Time.deltaTime);
        Vector3 right =  new Vector3(_CopyTransform.eulerAngles.x,_CopyTransform.eulerAngles.y, _CopyTransform.eulerAngles.z + (POVForward / 2));
        _CopyTransform.eulerAngles = right;
        Debug.DrawLine(transform.position, _CopyTransform.position + _CopyTransform.right * VisionDistance, Color.red, Time.deltaTime);
        Vector3 left = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y, transform.eulerAngles.z - (POVForward / 2));
        _CopyTransform.eulerAngles = left;
        Debug.DrawLine(transform.position, _CopyTransform.position + _CopyTransform.right * VisionDistance, Color.red, Time.deltaTime);
        _CopyTransform.eulerAngles = Reset;
        
       
    }
    private void OnChangedVisionDistance()
    {
        _LastRecordedDistance = VisionDistance;
    }
    private void OnChangedPOVF()
    {
        
         if (POVForward >= 0f)
        {
            _BoxCastSizeFront = new Vector2(0,0);
        
            _BoxCastSizeFront = new Vector2(VisionDistance, VisionDistance);
           
        }
        else
        {
            POVForward = 0f;
        }
        _LastRecordedPOVF = POVForward;
    }
    private void OnChangedPOVB()
    {
        
         if (POVBackward>= 0f)
        {
            _BoxCastSizeBack = new Vector2(0,0);
            _BoxCastSizeBack = new Vector2(VisionDistance,VisionDistance);
        }
        else
        {
            POVBackward = 0f;
        }
        _LastRecordedPOVB = POVBackward;
    }
    private RaycastHit2D[] CheckBoxCastHitsForward()
    {
        
        return Physics2D.BoxCastAll(transform.position, _BoxCastSizeFront ,transform.rotation.eulerAngles.z,ForwardDir,VisionDistance);
        
        
    }

    private RaycastHit2D[] CheckBoxCastHitsBackward()
    {
        
        return Physics2D.BoxCastAll(transform.position, _BoxCastSizeBack , POVBackward ,transform.right);
    }

    public bool TargetInView(GameObject target)
    {
        if (target != null)
        {
            RaycastHit2D[] hits = CheckBoxCastHitsForward();
            if (hits != null)
            {
                foreach(RaycastHit2D hit in hits)
                {
                   
                    if (hit.collider.transform.root.gameObject.name == target.name)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
}


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
