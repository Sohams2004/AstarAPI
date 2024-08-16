using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Node : IComparable
{
    public Node parent;

    public GameObject nodeGameObject {  get; private set; }

    public bool isVisited = false;
    public int versionNumber;

    bool isWalkable;
    int gCost, hCost, Fcost;

    public Vector3Int GridPos { get; private set; }
    public Vector3 WorldPos { get; private set; }

    public bool IsWalkable
    {
        get => isWalkable;

        set => isWalkable = value;
    }

    //Distance from starting point 
    public int GCost
    {
        get => gCost;

        set => gCost = value;
    }

    //Distance from target
    public int HCost
    {
        get => hCost;

        set => hCost = value;
    }

    //Gcost + Hcost
    public int FCost
    {
        get => gCost + hCost;
    }

    //
    public Node(Vector3Int gridPos, Vector3 worldPos, bool isWalkable, GameObject nodeGamobject)
    {
        GridPos = gridPos;
        WorldPos = worldPos;
        IsWalkable = isWalkable;
        nodeGameObject = nodeGamobject;
    }

    public int CompareTo(object obj)
    {
        Node node =obj as Node;

        if(node != null) { }
        if(FCost < ((Node)obj).FCost)
        {
            return -1;
        }
        else if (FCost > node.FCost)
        {
            return 1;
        }

        else
        {
            return 0;
        }
    }
}
