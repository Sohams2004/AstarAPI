using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AstarPathfinding
{
    public class Cars : MonoBehaviour
    {
        public float carSpeed = 5f;
        public float stopDuration = 3f;
        private List<Node> path;
        private int currentPathIndex = 0;
        [SerializeField] bool isStopped = false;
        private float stopTimer = 0f;

        [SerializeField] Cars car;
        public void SetPath(List<Node> newPath)
        {
            path = newPath;
            currentPathIndex = 0;
            transform.position = path[0].WorldPos;

            car = GetComponent<Cars>();
        }

        private void Update()
        {
            if (path == null || currentPathIndex >= path.Count)
            {
                return;
            }


            if (isStopped)
            {
                stopTimer += Time.deltaTime;
                if (stopTimer >= stopDuration)
                {
                    isStopped = false;
                    stopTimer = 0f;
                }
                return;
            }

            Vector3 targetPosition = path[currentPathIndex].WorldPos;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, carSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                currentPathIndex++;
                if (currentPathIndex < path.Count && Random.value < 0.1f)
                {
                    isStopped = true;
                }

                else if (currentPathIndex >= path.Count)
                {
                    StartCoroutine(Destroy());
                }
            }
        }

        IEnumerator Destroy()
        {
            car.enabled = false;
            yield return new WaitForSeconds(3f);
            Destroy(gameObject);
        }
    }
}

