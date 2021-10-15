using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    private static Node[,] grid;
    private int Size = 15;
    public static float prefabSize = 2; 

    public GameObject GridPrefab; 

    // Start is called before the first frame update
    void Awake()
    {
        grid = new Node[Size, Size];

        
        for (int x = 0; x < Size; x ++)     //each row of the grid
        {
            for (int z = 0; z < Size; z++)  // each columm of the grid
            {
                Node NewNode = new Node();
                NewNode.Position = new Vector2(x, z);    //setting the position
                Instantiate(GridPrefab, new Vector3(x, -0.5f, z) * prefabSize, Quaternion.identity);  //spawning a prefab
                grid[x, z] = NewNode;   //takes node variable, puts it in array

                //TODO
                // add to newnode to its connecitons using grids
                if (z > 0 )//&& grid [x, z - 1] != null)        //if grid z adding 1 is not equal to null, checking if there is a node
                {
                    NewNode.Connections.Add(grid[x, z - 1]);    //take away 1
                    grid[x, z - 1].Connections.Add(NewNode); //Makes the connection both ways
                }

                if (x > 0)// && grid [x - 1, z] != null)
                {
                    NewNode.Connections.Add(grid[x - 1, z]);
                    grid[x - 1, z].Connections.Add(NewNode);
                }
            }
        }
    }

    public static Node[] FindPath(Vector2 StartPositon, Vector2 EndPositon)
    {
        List<Node> OpenNodes = new List<Node>();
        List<Node> ClosedNodes = new List<Node>();
        Node CurrentNode = grid[ Mathf.RoundToInt(StartPositon.x), Mathf.RoundToInt (StartPositon.y)];
        Node EndNode = grid[Mathf.RoundToInt(EndPositon.x), Mathf.RoundToInt(EndPositon.y)];
        OpenNodes.Add(CurrentNode);

        while (OpenNodes.Count > 0)
        {
            OpenNodes = SortListByFScore(OpenNodes);
            CurrentNode = OpenNodes[0];

            if (CurrentNode.Position == EndNode.Position)
            {
                break;
            }

            OpenNodes.Remove(CurrentNode);
            ClosedNodes.Add(CurrentNode);

            foreach(Node c in CurrentNode.Connections)      //Simplifed for loops, go through the loop each time using the c node
            {
                if (!c.walkable)
                    ClosedNodes.Add(c);

                if (!ClosedNodes.Contains(c))
                {
                    float gScore;
                    float hScore;
                    float fScore;

                    gScore = CurrentNode.gScore + 1;
                    hScore = Vector2.Distance(c.Position, EndNode.Position);
                    fScore = (gScore + hScore);
                    if (!OpenNodes.Contains(c))
                    {
                        c.gScore = gScore;
                        c.fScore = fScore;
                        c.parent = CurrentNode;
                        OpenNodes.Add(c);
                    }
                    else if (fScore < c.fScore)
                    {
                        c.gScore = gScore;
                        c.fScore = fScore;
                        c.parent = CurrentNode;
                    }
                }
            }
        }

        List<Node> Path = new List<Node>();
        CurrentNode = EndNode;

        while (CurrentNode != null)
        {
            Path.Add(CurrentNode);
            CurrentNode = CurrentNode.parent;
        }
        Path.Reverse();
        ResetNodes();

        return Path.ToArray();
    }


    // Update is called once per frame
    void Update()
    {
        //Grid.TurnOffNodeAtWorldPosition(transform.position);
    }

    public static Vector2 WorldToGridPosition(Vector3 position)
    {
        return new Vector2(position.x / prefabSize, position.z / prefabSize);
    }

    public static void TurnOffNodeAtWorldPosition(Vector3 position)
    {
        Vector2 gridPosition = WorldToGridPosition(position);
        grid[Mathf.RoundToInt(gridPosition.x), Mathf.RoundToInt(gridPosition.y)].walkable = false;
    }

    static void ResetNodes()
    {
        foreach (Node n in grid)
        {
            n.fScore = 0;
            n.hScore = 0;
            n.gScore = 0;
            n.parent = null;
        }
    }

    //Bubble sort
    static List<Node> SortListByFScore(List<Node> unsortedList)
    {
        bool sorted = false;
        while (!sorted)
        {
            sorted = true;
            for (int i = 0; i < unsortedList.Count - 1; i++)
            {
                if (unsortedList[i].fScore > unsortedList[i + 1].fScore)
                {
                    Node temp = unsortedList[i];
                    unsortedList[i] = unsortedList[i + 1];
                    unsortedList[i + 1] = temp;
                    sorted = false;
                }
            }
        }

        return unsortedList;
    }
}
