using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Agent
{
    public class AgentMove : MonoBehaviour
    {
        public Rigidbody rb;
        public Aggro aggro;

        public float AgentSpeed { get; set; } = 1;

        private Vector3 direction;
        private List<Vector3> startDirections = new List<Vector3>()
            {
                Vector3.forward, -Vector3.forward, Vector3.right, -Vector3.right
            };

        private void Awake()
        {
            aggro.OnAgentCollisionEnterParam += OppositeDirection;
            aggro.OnWallCollisionEnterParam += OppositeDirection;

            SetDirectionOnStart();
        }

        private void OnDisable()
        {
            aggro.OnAgentCollisionEnterParam -= OppositeDirection;
            aggro.OnWallCollisionEnterParam -= OppositeDirection;
        }

        private void Update()
        {
            Movement();
        }

        public void SetDirectionOnStart()
        {
            direction = startDirections[Random.Range(0, startDirections.Count)];
            direction.Normalize();
            direction.y = 0;
        }

        public void OppositeDirection(Transform target)
        {
            direction = (transform.position - target.position).normalized;
            direction.y = 0;
        }

        private void Movement() =>
            GetComponent<Rigidbody>().MovePosition(transform.position + direction * Time.deltaTime);
    }
}