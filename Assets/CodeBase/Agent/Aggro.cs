using System;
using UnityEngine;

namespace CodeBase.Agent
{
    public class Aggro : MonoBehaviour
    {
        public event Action OnAgentCollisionEnter;
        public event Action OnWallCollisionEnter;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Agent"))
            {
                print("Agent");
                OnAgentCollisionEnter?.Invoke();
            }
            else if (collision.gameObject.CompareTag("Wall"))
            {
                print("Wall");
                OnWallCollisionEnter?.Invoke();
            }
        }
    }
}