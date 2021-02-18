using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public const int DefaultMaxCount = 200;
    public GameObject Player;
    public int AgentMaxCount;
    public GameObject[] AIPrefabs;
    public Transform[] SpawnLocations;

    [Range(0f,1000f)]public float LoadUnloadRenderDistance;
    
    [Header("DEBUG")]
    public Agent[] AgentsView;
    private List<Agent> _Agents;
    void Awake()
    {
        
        _Agents = _Agents != null ? _Agents : new List<Agent>(0);
        AgentMaxCount = AgentMaxCount > 0 ? AgentMaxCount : DefaultMaxCount;
        if (SpawnLocations != null)
        {
            foreach(Transform loca in SpawnLocations)
            {
                CreateAgent(null, loca.position);
            }
        }
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
               agent.gameObject.SetActive(true);
           }
           else
           {
               agent.gameObject.SetActive(true);
           }
        }
    }


    public void CreateAgent(GameObject prefab, Vector3 position)
    {
        if (_Agents.Count < AgentMaxCount)
        {
            GameObject agentObj = GameObject.Instantiate(prefab, position, Quaternion.identity) as GameObject;
            Agent agent = agentObj.GetComponentInChildren<Agent>();
            if (agent != null)
            {
                _Agents.Add(agent);
                OnAgentsListAltered();
            }
        }
    }
    
    public void DestroyAgent(GameObject agent)
    {
        if (agent != null)
        {
            DestroyAgent()
        }
    }
    public void DestroyAgent(Agent agent)
    {
        if (agent != null)
        {
            agent.DestoryAgent();
        }
    }

    public void LoadInAgents(Agent[] agents)
    {
        if (agents != null)
        {
            foreach(Agent agent in agents)
            {
                if (agent != null)
                {
                    _Agents.Add(agent);
                }
            }
        }
    }

    private void OnAgentsListAltered()
    {
        AgentsView = _Agents.ToArray();
    }

    
}
