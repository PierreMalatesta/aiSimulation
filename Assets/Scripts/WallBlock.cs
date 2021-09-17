using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBlock : MonoBehaviour
{
    private static Node[,] Wall;
    private int size = 3;

    // Start is called before the first frame update
    void Start()
    {
        Wall = new Node[size, size];
        Grid.TurnOffNodeAtWorldPosition(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
