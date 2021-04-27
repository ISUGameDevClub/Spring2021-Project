using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class AIManager : MonoBehaviour
{
    public const string AgentTag = "AI-Agent";
    public enum SpawningMethod {
        Static, Dynamic
    }
    public const int DefaultMaxCount = 200;
    public GameObject Player;
    
    public GameObject[] AIPrefabs;
    public SpawningMethod SpawnMethod;
    
    [Header("Dynamic Spawning Options")]
    public int AgentMaxCount;

    public GameObject SpawnLocationParent;
    [HideInInspector]public Transform[] SpawnLocations;
    

    [Range(0f,1000f)]public float LoadUnloadRenderDistance;

    public bool LogEvents;
    
    
    [Header("DEBUG")]
    public Agent[] ActiveAgents;
    
    private List<Agent> _Agents;
    private float _OffsetSpawnDistance;
    void Awake()
    {
        _OffsetSpawnDistance = 0.5f;
        _Agents = _Agents != null ? _Agents : new List<Agent>(0);
        AgentMaxCount = AgentMaxCount > 0 ? AgentMaxCount : DefaultMaxCount;
        if (SpawnLocationParent != null)
        {
            SpawnLocations = new Transform[SpawnLocationParent.transform.childCount];
            for (int i =0; i < SpawnLocations.Length; i++)
            {
                SpawnLocations[0] = SpawnLocationParent.transform.GetChild(i);
            }
        }
        LoadInAgents();
        
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate() 
    {
        HandleRenderLogic();
    }

    void HandleRenderLogic()
    {
        Vector3 PlayerPos = gameObject.transform.position;
        float distance =  -1f;
        foreach(Agent agent in _Agents)
        {
           distance = Mathf.Abs(Vector3.Distance(agent.transform.position, PlayerPos));
           if (distance > LoadUnloadRenderDistance)
           {
               UnrenderAgent(agent.gameObject);
           }
           else
           {
               RenderAgent(agent.gameObject);
           }
        }
    }

    public void RenderAgent(GameObject gameObject)
    {
       
        if (gameObject != null)
        {
            if (!gameObject.activeSelf)
            {
                 UnityLoggingDelegate.LogIfTrue(LogEvents, UnityLoggingDelegate.LogType.General, gameObject.name + " Was rerendered due to distance");
                gameObject.SetActive(true);
            }
        }
    }
    public void UnrenderAgent(GameObject gameObject)
    {
        
        if (gameObject != null)
        {
            if (gameObject.activeSelf)
            {
                UnityLoggingDelegate.LogIfTrue(LogEvents, UnityLoggingDelegate.LogType.General, gameObject.name + " Was unrendered due to distance");
                gameObject.SetActive(false);
            }
        }
    }


    public Agent CreateAgent(GameObject prefab, Vector3 position)
    {
        if (_Agents.Count < AgentMaxCount)
        {
            GameObject agentObj = GameObject.Instantiate(prefab, position, Quaternion.Euler(prefab.transform.eulerAngles.x,prefab.transform.eulerAngles.y,prefab.transform.eulerAngles.z)) as GameObject;
            Agent agent = agentObj.GetComponentInChildren<Agent>();
            if (agent != null)
            {
                _Agents.Add(agent);
                OnAgentsListAltered();
            }
            return agent;
        }

        return null;
    }
    
    public void DestroyAgent(GameObject agent)
    {
        if (agent != null)
        {
            DestroyAgent(agent.GetComponentInChildren<Agent>());
        }
    }
    public void DestroyAgent(Agent agent)
    {
        if (agent != null)
        {
            agent.DestoryAgent();
        } 
        else
        {
            UnityLoggingDelegate.Log(UnityLoggingDelegate.LogType.Error, "Agent script passed as a param is null.");
        }
    }

    public void LoadInAgents()
    {
        if (SpawnMethod == SpawningMethod.Dynamic &&  SpawnLocations != null)
        {
            UnityLoggingDelegate.LogIfTrue(LogEvents, UnityLoggingDelegate.LogType.General, "Initalizing Spawn proccess of AI Agents in scene");
            foreach(Transform loca in SpawnLocations)
            {
                bool SpawnFlag = true;
                foreach(Agent agent in _Agents)
                {
                    if (agent != null)
                    {
                        float dist = Vector2.Distance(agent.gameObject.transform.position, loca.position);
                        if (dist <= _OffsetSpawnDistance)
                        {
                            SpawnFlag = false;
                            break;
                        }
                        
                    }
                }
                if (SpawnFlag)
                {
                    Agent agent = CreateAgent(AIPrefabs[0], loca.position);
                    if (loca.childCount > 0)
                    {
                        agent.AgentBehavior.PatrolPointsParent = loca.GetChild(0).gameObject;
                    }
                }
                else
                {
                    UnityLoggingDelegate.LogIfTrue(LogEvents, UnityLoggingDelegate.LogType.Warning, "Tried to spawn a AI agent to close to another AI agent");
                }
            }
            UnityLoggingDelegate.LogIfTrue(LogEvents, UnityLoggingDelegate.LogType.General, " Spawn proccess of AI Agents in scene finished");
        } 
        else if (SpawnMethod == SpawningMethod.Static)
        {
            GameObject[] objs = GameObject.FindGameObjectsWithTag(AgentTag);
            if (objs != null)
            {
                foreach(GameObject o in objs)
                {
                    if (o != null)
                    {
                        Agent agent = o.GetComponentInChildren<Agent>();
                        if (agent != null)
                        {
                            _Agents.Add(agent);
                        }
                    }
                }
            }
        }
    }

    private void OnAgentsListAltered()
    {
        ActiveAgents = _Agents.ToArray();
    }

    
}
