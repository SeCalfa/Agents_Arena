using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class Plane : MonoBehaviour
    {
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
    }
}