using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AstarPathfinding
{
    public class CarManager : MonoBehaviour
    {
        [SerializeField] List<GameObject> carsPrefab;
        [SerializeField] float spawnTime, spawnInterval;

        [SerializeField] Transform[] points;

        [SerializeField] Vector3Int start;
        [SerializeField] Vector3Int end;

        [SerializeField] int numberOfCars;
        [SerializeField] int maxnumberOfCars;
        [SerializeField] int currentNoOfCars;

        [SerializeField] AStar aStar;
        [SerializeField] Grid grid;

        void SpawnCars()
        {
            List<Node> path = aStar.GetPath(start, end);
            if (path != null && carsPrefab.Count > 0)
            {
                Vector3 startPosition = grid.WorldPosition(start);
                Debug.Log(startPosition + " " + start);

                GameObject randomCarPrefab = carsPrefab[Random.Range(0, carsPrefab.Count)];

                start = new Vector3Int(Random.Range(0, grid.gridNodeCountX), Random.Range(0, 1), Random.Range(0, grid.gridNodeCountZ));
                end = new Vector3Int(Random.Range(0, grid.gridNodeCountX), Random.Range(0, 1), Random.Range(0, grid.gridNodeCountZ));

                GameObject carObject = Instantiate(randomCarPrefab, startPosition, Quaternion.Euler(0, 180, 0));
                Cars car = carObject.GetComponent<Cars>();
                if (car != null)
                {
                    car.SetPath(path);
                }
                else
                {
                    Debug.LogError("Car component not found on the spawned car prefab!");
                }
            }
        }

        private void Update()
        {
            spawnTime += Time.deltaTime;
            if (spawnTime >= spawnInterval && transform.childCount < numberOfCars)
            {
                SpawnCars();
                spawnTime = 0f;
            }
        }
    }
}
