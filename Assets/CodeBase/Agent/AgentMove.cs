using UnityEngine;

namespace CodeBase.Agent
{
    public class AgentMove : MonoBehaviour
    {
        public CharacterController CharacterController;
        public Aggro aggro;

        public float AgentSpeed { get; set; } = 1;

        private void Awake()
        {
            aggro.OnAgentCollisionEnter += ChangeDirection;
            aggro.OnWallCollisionEnter += ChangeDirection;

            SetRandomRotation();
        }

        private void OnDisable()
        {
            aggro.OnAgentCollisionEnter -= ChangeDirection;
            aggro.OnWallCollisionEnter -= ChangeDirection;
        }

        private void Update()
        {
            Movement();
            if (CharacterController.velocity.magnitude <= 0.01f)
                ChangeDirection();
        }

        private void Movement()
        {
            Vector3 movementVector = transform.forward;
            movementVector.y = 0;
            movementVector.Normalize();
            CharacterController.Move(movementVector * AgentSpeed * Time.deltaTime);
        }

        private void SetRandomRotation() =>
            transform.rotation = Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0));

        private void ChangeDirection() =>
            transform.rotation = Quaternion.Euler(new Vector3(0, transform.eulerAngles.y + 120, 0));
    }
}