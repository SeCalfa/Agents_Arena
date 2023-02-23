using CodeBase.Agent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class AgentSpawner : MonoBehaviour
    {
        [Header("Spawn generetor settings")]
        [Range(3, 5)]
        [SerializeField] private int agentsOnStart;
        [Range(5, 30)]
        [SerializeField] private int maxAgents;
        [Range(2f, 6f)]
        [SerializeField] private float spawnRate;

        [Header("Agent settings")]
        [Range(0.5f, 3)]
        [SerializeField] private float agentSpeed;

        private List<Plane> planes;
        private List<int> filledPlaneOnStart = new List<int>();
        private List<GameObject> allAgents = new List<GameObject>();
        private GameObject agent;

        private int agentId = 0;

        public void Construct(List<Plane> planes)
        {
            this.planes = planes;

            agent = Resources.Load(Constance.Agent) as GameObject;
        }

        private void Start()
        {
            StartCoroutine(SpawnByRate());
        }

        public void SpawnOnStart()
        {
            for (int i = 0; i < agentsOnStart; i++)
            {
                int rand = Random.Range(0, planes.Count);
                while (filledPlaneOnStart.Contains(rand))
                {
                    rand = Random.Range(0, planes.Count);
                }
                filledPlaneOnStart.Add(rand);
                SpawnAgent(rand);
            }
        }

        public void DestroyAgent(GameObject agent)
        {
            if (allAgents.Contains(agent))
            {
                allAgents.Remove(agent);
                Destroy(agent);
            }
        }

        private IEnumerator SpawnByRate()
        {
            while (true)
            {
                yield return new WaitForSeconds(spawnRate);

                if(allAgents.Count < maxAgents)
                {
                    int rand = Random.Range(0, planes.Count);
                    SpawnAgent(rand);
                }
            }
        }

        private void SpawnAgent(int rand)
        {
            GameObject currentAgent = Instantiate(agent, planes[rand].GetSpawnPoint.position, Quaternion.identity, transform);
            currentAgent.GetComponent<AgentMove>().AgentSpeed = agentSpeed;
            currentAgent.GetComponent<AgentHealth>().Construct(this);
            currentAgent.GetComponent<AgentInfo>().Construct(agentId++);

            allAgents.Add(currentAgent);
        }
    }
}