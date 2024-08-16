using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AstarPathfinding
{
    public class Grid : MonoBehaviour
    {
        [SerializeField] GameObject nodePrefab;

        [SerializeField] int gridArraySize;

        [SerializeField] public int gridNodeCountX;
        [SerializeField] public int gridNodeCountZ;

        [SerializeField] int nodeWidth;
        [SerializeField] int nodeHeight;

        [SerializeField] bool isWalkable;

        [SerializeField] Node[] nodes;

        private void Start()
        {
            gridArraySize = gridNodeCountX * gridNodeCountZ;

            nodes = new Node[gridArraySize];

            Vector3 gameObjPos = gameObject.transform.position;

            for (int z = 0; z < gridNodeCountZ; z++)
            {
                for (int x = 0; x < gridNodeCountX; x++)
                {
                    int a = x + z * gridNodeCountX;

                    Vector3Int gridPos = new Vector3Int(x, 0, z);
                    Vector3 worldPos = new Vector3(x * nodeWidth, 0, z * nodeHeight);
                    Vector3 spawnPos = gameObjPos + worldPos;


                    GameObject go = null;

                    go = Instantiate(nodePrefab, spawnPos, Quaternion.identity);
                    go.transform.localScale = new Vector3(nodeWidth, 1, nodeHeight);
                    go.transform.parent = gameObject.transform;

                    bool isWalkable = !Physics.CheckBox(worldPos, new Vector3(nodeWidth / 2.0f, 0, nodeHeight / 2.0f));

                    nodes[a] = new Node(gridPos, worldPos, isWalkable, go);
                }
            }
        }

        public Node GetNode(Vector3Int GridPosition)
        {
            int i = GridPosition.x + GridPosition.z * gridNodeCountX;
            return nodes[i];
        }

        public Vector3 WorldPosition(Vector3Int gridPos)
        {
            Vector3 gameObjectPos = gameObject.transform.position;
            return new Vector3(gridPos.x * nodeWidth, gridPos.y, gridPos.z * nodeHeight);
        }
    }
}

