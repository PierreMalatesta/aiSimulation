using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBlock : MonoBehaviour
{
    private static Node[,] Wall;
    private int size = 3;
    public bool mightDeactivate;

    // Start is called before the first frame update
    void Start()
    {
       //finds a decimal between 0 and 1 (0.5 is 50%)
       //so there is a 50% chance it will be set to false or turn off
        if (mightDeactivate && Random.Range(0, 1f) > 0.5f)
        {
            gameObject.SetActive(false);
            return;
        }
        Wall = new Node[size, size];
        Grid.TurnOffNodeAtWorldPosition(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
