using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class PlayerAI : MonoBehaviour
{
    //Following a Vector3 array using waypoints

    public GameObject[] waypoints;      //array of gameobjects
    int current = 0;                    //number that shows which way in the array we are going to
    float rotSpeed;                     //rotates the character
    public float speed;                 
    float WPradius = 1;                 //WaypointRadius basically has a value of 1 meaning it reaches a waypoint and goesd to the next one


    public Vector2 start;
    public Vector2 destination;
    public float moveSpeed;


    Node[] path;

    // Start is called before the first frame update
    void Start()
    {
        path = Grid.FindPath(start, destination);
    }

    // Update is called once per frame
    void Update()
    {
        //how to follow Vector3 array (Vector3 = path[x].WorldPosition)
        if(Vector3.Distance(waypoints[current].transform.position, transform.position) < WPradius)  //Checking distance of current waypoint and its position and the position of the current object and if its less than WPradius
        {
            current++;  // adds 1 to the current value
                if (current >= waypoints.Length) // basically checks the "[]" value therefore if it reaches its last waypoint it gets set back to 0
                {
                    current = 0;    
                }
        }

        //moves the position to the current waypoint, time.delta time is used to change the speed
        transform.position = Vector3.MoveTowards(transform.position, waypoints[current].transform.position, Time.deltaTime * speed);
    }
}
