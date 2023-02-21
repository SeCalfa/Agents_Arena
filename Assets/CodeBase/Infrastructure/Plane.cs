using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class Plane : MonoBehaviour
    {
        [SerializeField] private GameObject leftWall, rightWall, forwardWall, backWall;
        [Space]
        [SerializeField] private Transform spawnPoint;

        private Material white, grey;

        private void Awake()
        {
            white = (Material)Resources.Load(Constance.WhiteMat);
            grey = (Material)Resources.Load(Constance.GreyMat);
        }

        public void SetWhite() =>
            GetComponent<MeshRenderer>().material = white;

        public void SetGrey() =>
            GetComponent<MeshRenderer>().material = grey;

        public void LeftWallOn() =>
            leftWall.SetActive(true);

        public void RightWallOn() =>
            rightWall.SetActive(true);

        public void ForwardWallOn() =>
            forwardWall.SetActive(true);

        public void BackWallOn() =>
            backWall.SetActive(true);

        public Transform GetSpawnPoint { get => spawnPoint; }
    }
}