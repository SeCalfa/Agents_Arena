﻿using UnityEngine;
using TMPro;

namespace CodeBase.Agent
{
    public class AgentHealth : MonoBehaviour
    {
        public Transform canvas;
        public TextMeshProUGUI text;

        private int health = 3;

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

            if (health == 0)
                Destroy(gameObject);
        }
    }
}