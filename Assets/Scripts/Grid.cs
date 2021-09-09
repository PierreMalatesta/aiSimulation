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
                NewNode.Postion = new Vector2(x, z);    //setting the position
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
            foreach(Node c in CurrentNode.Connections)      //Simplifed for loops, go through the loop each time using the c node
            {
                if (!ClosedNodes.Contains(c))
                {
                    float gScore;
                    float hScore;
                    float fScore;
                    float NewfScore;

                    hScore = Vector2.Distance(c.Postion, EndNode.Postion);
                    gScore = Vector2.Distance(c.Postion, StartPositon);
                    fScore = (gScore + hScore);

                    if (NewfScore < fScore)
                    {
                        //Update f value
                        //Set parent to be the current vertex
                    }
                }
                //NEXT adjacent vertext
                //Remove vertx with lowest f value from open list and make it currentt
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
