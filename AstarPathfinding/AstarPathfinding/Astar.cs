using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AstarPathfinding
{
    public class AStar : MonoBehaviour
    {
        static int globalVersionNumber = 0;

        [SerializeField] Vector3Int startNodeGridPos;
        [SerializeField] Vector3Int goalNodeGridPos;

        List<Node> openList = new List<Node>();
        List<Node> closedList = new List<Node>();
        List<Node> finalPath = new List<Node>();
        List<Node> neighbors = new List<Node>();

        Node startNode;
        Node goalNode;

        Grid grid;

        private void Start()
        {
            grid = FindObjectOfType<Grid>();
        }

        public List<Node> GetPath(Vector3Int startPos, Vector3Int goalPos)
        {
            startNode = grid.GetNode(startPos);
            goalNode = grid.GetNode(goalPos);

            finalPath.Clear();
            openList.Clear();

            globalVersionNumber++;

            Node currentNode = startNode;
            openList.Add(currentNode);

            while (openList.Count > 0)
            {
                openList.Sort();
                currentNode = openList[0];

                if (currentNode.versionNumber < globalVersionNumber)
                {
                    currentNode.GCost = 0;
                    currentNode.HCost = 0;
                    currentNode.isVisited = false;
                    currentNode.parent = null;
                    currentNode.versionNumber = globalVersionNumber;
                }

                openList.Remove(currentNode);
                currentNode.isVisited = true;

                if (currentNode == goalNode)
                {
                    FindPath(currentNode);
                    finalPath.Reverse();
                    return finalPath;
                }

                neighbors.Clear();

                Vector3Int topNodePos = currentNode.GridPos + new Vector3Int(0, 0, 1);
                if (topNodePos.z < grid.gridNodeCountZ)
                {
                    Node topNode = grid.GetNode(topNodePos);
                    neighbors.Add(topNode);
                }

                Vector3Int bottomNodePos = currentNode.GridPos + new Vector3Int(0, 0, -1);
                if (bottomNodePos.z >= 0)
                {
                    Node bottomNode = grid.GetNode(bottomNodePos);
                    neighbors.Add(bottomNode);
                }

                Vector3Int leftNodePos = currentNode.GridPos + new Vector3Int(-1, 0, 0);
                if (leftNodePos.x > -0)
                {
                    Node leftNode = grid.GetNode(leftNodePos);
                    neighbors.Add(leftNode);
                }

                Vector3Int rightNodePos = currentNode.GridPos + new Vector3Int(1, 0, 0);
                if (rightNodePos.x < grid.gridNodeCountX)
                {
                    Node rightNode = grid.GetNode(rightNodePos);
                    neighbors.Add(rightNode);
                }

                for (int i = 0; i < neighbors.Count; i++)
                {
                    if (neighbors[i].versionNumber < globalVersionNumber)
                    {
                        neighbors[i].GCost = 0;
                        neighbors[i].HCost = 0;
                        neighbors[i].isVisited = false;
                        neighbors[i].parent = null;
                        neighbors[i].versionNumber = globalVersionNumber;
                    }

                    if (!neighbors[i].IsWalkable || neighbors[i].isVisited)
                    {
                        continue;
                    }

                    int newDistanceCost = CalculateDistance(neighbors[i].GridPos, currentNode.GridPos) + currentNode.GCost;

                    if (newDistanceCost < neighbors[i].GCost || !openList.Contains(neighbors[i]))
                    {
                        neighbors[i].GCost = newDistanceCost;
                        neighbors[i].HCost = CalculateDistance(neighbors[i].GridPos, currentNode.GridPos);
                        neighbors[i].parent = currentNode;

                        if (!openList.Contains(neighbors[i]))
                        {
                            openList.Add(neighbors[i]);
                        }
                    }
                }
            }
            return null;
        }

        void FindPath(Node node)
        {
            if (node.parent != null)
            {
                finalPath.Add(node);
                FindPath(node.parent);
            }

            else
            {
                finalPath.Add(node);
            }
        }

        int CalculateDistance(Vector3Int VectorA, Vector3Int VectorB)
        {
            var x = VectorA.x - VectorB.x;
            var y = VectorA.y - VectorB.y;
            var z = VectorA.z - VectorB.z;

            return Mathf.Abs(x + y + z);
        }
    }
}
