using UnityEngine;

namespace CodeBase.Agent.Logic
{
    public class DirectionData
    {
        public Vector3 Direction { get; }
        public float Distance { get; }

        public DirectionData(Vector3 direction, float distance)
        {
            Direction = direction;
            Distance = distance;
        }
    }
}
