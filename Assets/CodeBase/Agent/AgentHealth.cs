using UnityEngine;
using TMPro;
using CodeBase.Infrastructure;
using System;

namespace CodeBase.Agent
{
    public class AgentHealth : MonoBehaviour
    {
        public Transform canvas;
        public TextMeshProUGUI text;

        public event Action<int> OnTakeDamage;
        public event Action OnDeath;

        private AgentSpawner agentSpawner;

        private int health = 3;

        public void Construct(AgentSpawner agentSpawner)
        {
            this.agentSpawner = agentSpawner;
        }

        private void Awake()
        {
            GetComponent<Aggro>().OnAgentCollisionEnter += TakeDamage;
        }

        private void OnDisable()
        {
            GetComponent<Aggro>().OnAgentCollisionEnter -= TakeDamage;
        }

        private void Update()
        {
            Quaternion rotation = Camera.main.transform.rotation;
            canvas.LookAt(canvas.position + rotation * Vector3.forward, rotation * Vector3.up);
        }

        public void TakeDamage()
        {
            health -= 1;
            text.text = health.ToString();

            if (health == 2)
                text.color = Color.yellow;
            else if (health == 1)
                text.color = Color.red;

            OnTakeDamage?.Invoke(health);

            if (health == 0)
            {
                OnDeath?.Invoke();
                agentSpawner.DestroyAgent(gameObject);
            }
        }
    }
}