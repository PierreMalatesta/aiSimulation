using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Vector2 Position;
    public List<Node> Connections;

    public float gScore;
    public float hScore;
    public float fScore;

    public Node parent;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 WorldPosition()
    {
        return new Vector3(Position.x, 0, Position.y);
    }
}
