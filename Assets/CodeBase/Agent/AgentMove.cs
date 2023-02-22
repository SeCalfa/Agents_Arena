using CodeBase.Agent.Logic;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.Agent
{
    public class AgentMove : MonoBehaviour
    {
        public Rigidbody rb;
        public Aggro aggro;

        public float AgentSpeed { get; set; } = 1;

        private Vector3 direction;
        private List<DirectionData> moveDirection = new List<DirectionData>();

        private void Awake()
        {
            aggro.OnAgentCollisionEnter += ScanSpace;
            aggro.OnWallCollisionEnter += ScanSpace;

            ScanSpace();
        }

        private void OnDisable()
        {
            aggro.OnAgentCollisionEnter -= ScanSpace;
            aggro.OnWallCollisionEnter -= ScanSpace;
        }

        private void Update()
        {
            Movement();
        }

        public void ScanSpace()
        {
            moveDirection.Clear();

            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
                moveDirection.Add(new DirectionData(Vector3.forward, Vector3.Distance(transform.position, hit.point)));
            if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.forward), out hit, Mathf.Infinity))
                moveDirection.Add(new DirectionData(-Vector3.forward, Vector3.Distance(transform.position, hit.point)));
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, Mathf.Infinity))
                moveDirection.Add(new DirectionData(Vector3.right, Vector3.Distance(transform.position, hit.point)));
            if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.right), out hit, Mathf.Infinity))
                moveDirection.Add(new DirectionData(-Vector3.right, Vector3.Distance(transform.position, hit.point)));

            float dis = moveDirection.First().Distance;
            foreach (DirectionData item in moveDirection)
            {
                if(item.Distance < dis)
                    dis = item.Distance;
            }
            List<DirectionData> directions = moveDirection.Where(d => d.Distance > 0.5f).ToList();

            direction = directions[Random.Range(0, directions.Count)].Direction;
            direction.Normalize();
            direction.y = 0;
        }

        private void Movement() =>
            GetComponent<Rigidbody>().MovePosition(transform.position + direction * Time.deltaTime);
    }
}