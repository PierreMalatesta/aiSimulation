using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    private static Node[,] grid;
    private int Size = 5;

    public GameObject GridPrefab; 

    // Start is called before the first frame update
    void Start()
    {
        grid = new Node[Size, Size];

        for (int x = 0; x < Size; x ++)     //each row of the grid
        {
            for (int z = 0; z < Size; z++)  // each columm of the grid
            {
                Node NewNode = new Node();
                NewNode.Position = new Vector2(x, z);    //setting the position
                Instantiate(GridPrefab, new Vector3(x, 0, z), Quaternion.identity);  //spawning a prefab
                grid[x, z] = NewNode;   //takes node variable, puts it in array

                //TODO
                // add to newnode to its connecitons using grids
                if (grid [x, z - 1] != null)        //if grid z adding 1 is not equal to null, checking if there is a node
                {
                    NewNode.Connections.Add(grid[x, z - 1]);    //take away 1
                    grid[x, z - 1].Connections.Add(NewNode); //Makes the connection both ways
                }

                if (grid [x - 1, z] != null)
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

        while (OpenNodes.Count > 0)
        {
            OpenNodes = SortListByFScore(OpenNodes);
            CurrentNode = OpenNodes[0];

            if (CurrentNode == EndNode)
            {
                break;
            }

            OpenNodes.Remove(CurrentNode);
            ClosedNodes.Add(CurrentNode);

            foreach(Node c in CurrentNode.Connections)      //Simplifed for loops, go through the loop each time using the c node
            {
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


        return Path.ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        
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
