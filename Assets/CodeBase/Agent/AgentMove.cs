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
        private List<Vector3> targetPoints = new List<Vector3>()
            {
                Vector3.one, -Vector3.one, Vector3.zero
            };

        private void Awake()
        {
            aggro.OnAgentCollisionEnterParam += OppositeDirection;
            aggro.OnWallCollisionEnter += SetNewDirection;

            SetNewDirection();
        }

        private void OnDisable()
        {
            aggro.OnAgentCollisionEnterParam -= OppositeDirection;
            aggro.OnWallCollisionEnter -= SetNewDirection;
        }

        private void Update()
        {
            Movement();
        }

        private void OppositeDirection(Transform target)
        {
            direction = transform.position - target.position;
            direction.y = 0;
            direction.Normalize();
        }

        private void SetNewDirection()
        {
            direction = targetPoints[Random.Range(0, targetPoints.Count)] - transform.position;
            direction.y = 0;
            direction.Normalize();
        }

        private void Movement() =>
            GetComponent<Rigidbody>().MovePosition(transform.position + direction * AgentSpeed * Time.deltaTime);
    }
}