using System;
using UnityEngine;

namespace CodeBase.Agent
{
    public class Aggro : MonoBehaviour
    {
        public event Action OnWallCollisionEnter;
        public event Action OnAgentCollisionEnter;
        public event Action<Transform> OnAgentCollisionEnterParam;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag(Constance.AgentTag))
            {
                OnAgentCollisionEnter?.Invoke();
                OnAgentCollisionEnterParam?.Invoke(collision.transform);
            }
            else if (collision.gameObject.CompareTag(Constance.WallTag))
            {
                OnWallCollisionEnter?.Invoke();
            }
        }
    }
}