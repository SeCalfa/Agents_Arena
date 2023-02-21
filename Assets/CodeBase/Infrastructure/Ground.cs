using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class Ground : MonoBehaviour
    {
        [Range(2, 15)]
        [SerializeField] private int planeSize;

        private void Awake()
        {
            GroundClear();
            GroundGenerate();
        }

        private void GroundGenerate()
        {
            GameObject plane = (GameObject)Resources.Load(Constance.Plane);

            float pointX = planeSize - 0.5f;
            float pointZ = planeSize - 0.5f;
            int rowCount = 1;

            for (int x = 0; x < planeSize * 2; x++)
            {
                for (int z = 0; z < planeSize * 2; z++)
                {
                    GameObject currentPlane = Instantiate(plane, new Vector3(pointX, 0, pointZ), Quaternion.identity, transform);

                    if (rowCount % 2 == 0)
                    {
                        if ((z + 1) % 2 == 0)
                            currentPlane.GetComponent<Plane>().SetGrey();
                        else
                            currentPlane.GetComponent<Plane>().SetWhite();
                    }
                    else
                    {
                        if ((z + 1) % 2 == 0)
                            currentPlane.GetComponent<Plane>().SetWhite();
                        else
                            currentPlane.GetComponent<Plane>().SetGrey();
                    }

                    pointZ -= 1f;
                }

                pointX -= 1f;
                pointZ = planeSize - 0.5f;
                rowCount++;
            }
        }

        private void GroundClear()
        {
            IEnumerable<GameObject> startGameObjects = GetComponentsInChildren<Transform>().SkipWhile(t => t == transform).Select(t => t.gameObject);

            foreach (var item in startGameObjects)
                Destroy(item);
        }
    }
}