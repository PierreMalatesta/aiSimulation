using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Node
{
    public Vector2 Position;
    public List<Node> Connections = new List<Node>();

    public float gScore;
    public float hScore;
    public float fScore;

    public bool walkable = true;

    public Node parent;

    public Vector3 WorldPosition()
    {
        return new Vector3(Position.x, 0, Position.y) * Grid.prefabSize;
    }
}
