using System;
using UnityEngine;

namespace CodeBase.Agent
{
    public class Aggro : MonoBehaviour
    {
        public event Action OnAgentCollisionEnter;
        public event Action<Transform> OnAgentCollisionEnterParam;
        public event Action<Transform> OnWallCollisionEnterParam;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Agent"))
            {
                OnAgentCollisionEnter?.Invoke();
                OnAgentCollisionEnterParam?.Invoke(collision.transform);
            }
            else if (collision.gameObject.CompareTag("Wall"))
            {
                OnWallCollisionEnterParam?.Invoke(collision.transform);
            }
        }
    }
}