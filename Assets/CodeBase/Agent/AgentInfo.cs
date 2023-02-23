using System;
using UnityEngine;

namespace CodeBase.Agent
{
    public class AgentInfo : MonoBehaviour
    {
        [SerializeField]
        private AgentHealth agentHealth;

        private int id;
        private Material selectOn, selectOff;

        public string agentName { get; private set; }
        public string health { get; private set; } = "Health: 3";

        public event Action OnHealthUpdate;

        private void Awake()
        {
            selectOn = Resources.Load(Constance.GreenMat) as Material;
            selectOff = Resources.Load(Constance.BlueMat) as Material;

            agentHealth.OnTakeDamage += HealthUpdate;
        }

        private void OnDisable()
        {
            agentHealth.OnTakeDamage -= HealthUpdate;
        }

        public void Construct(int agentId)
        {
            id = agentId;

            agentName = $"Name: Agent {id}";
            name = $"Agent {id}";
        }

        public void HealthUpdate(int health)
        {
            this.health = $"Health: {health}";
            OnHealthUpdate?.Invoke();
        }

        public void SelectOn()
        {
            GetComponent<MeshRenderer>().material = selectOn;
        }

        public void SelectOff()
        {
            GetComponent<MeshRenderer>().material = selectOff;
        }
    }
}