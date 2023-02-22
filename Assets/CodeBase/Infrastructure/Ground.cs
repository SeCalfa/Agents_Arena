using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    [RequireComponent(typeof(AgentSpawner))]
    public class Ground : MonoBehaviour
    {
        [Range(4, 10)]
        [SerializeField] private int planeSize;

        private AgentSpawner agentSpawner;
        private List<Plane> planes = new List<Plane>();

        private void Awake()
        {
            agentSpawner = GetComponent<AgentSpawner>();

            GroundClear();
            GroundGenerate();
            agentSpawner.Construct(planes);
            agentSpawner.SpawnOnStart();
        }

        private void GroundGenerate()
        {
            GameObject plane = Resources.Load(Constance.Plane) as GameObject;

            float pointX = planeSize - 0.5f;
            float pointZ = planeSize - 0.5f;
            int rowCount = 1;

            for (int x = 0; x < planeSize * 2; x++)
            {
                for (int z = 0; z < planeSize * 2; z++)
                {
                    Plane currentPlane = Instantiate(plane, new Vector3(pointX, 0, pointZ), Quaternion.identity, transform).GetComponent<Plane>();

                    if (rowCount % 2 == 0)
                    {
                        if ((z + 1) % 2 == 0)
                            currentPlane.SetGrey();
                        else
                            currentPlane.SetWhite();
                    }
                    else
                    {
                        if ((z + 1) % 2 == 0)
                            currentPlane.SetWhite();
                        else
                            currentPlane.SetGrey();
                    }

                    pointZ -= 1f;
                    planes.Add(currentPlane);
                    BordersOn(x, z, currentPlane);
                }

                pointX -= 1f;
                pointZ = planeSize - 0.5f;
                rowCount++;
            }
        }

        private void BordersOn(int x, int z, Plane currentPlane)
        {
            if (x == 0 && z == 0)
            {
                currentPlane.ForwardWallOn();
                currentPlane.RightWallOn();
            }
            else if (x == 0 && z == planeSize * 2 - 1)
            {
                currentPlane.BackWallOn();
                currentPlane.RightWallOn();
            }
            else if (x == planeSize * 2 - 1 && z == 0)
            {
                currentPlane.ForwardWallOn();
                currentPlane.LeftWallOn();
            }
            else if (x == planeSize * 2 - 1 && z == planeSize * 2 - 1)
            {
                currentPlane.BackWallOn();
                currentPlane.LeftWallOn();
            }
            else if (x == 0)
                currentPlane.RightWallOn();
            else if (x == planeSize * 2 - 1)
                currentPlane.LeftWallOn();
            else if (z == 0)
                currentPlane.ForwardWallOn();
            else if (z == planeSize * 2 - 1)
                currentPlane.BackWallOn();
        }

        private void GroundClear()
        {
            IEnumerable<GameObject> startGameObjects = GetComponentsInChildren<Transform>().SkipWhile(t => t == transform).Select(t => t.gameObject);

            foreach (var item in startGameObjects)
                Destroy(item);
        }
    }
}