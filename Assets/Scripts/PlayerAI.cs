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
    float WPradius = 0.01f;                 //WaypointRadius basically has a value of 1 meaning it reaches a waypoint and goesd to the next one

    public GameObject Ending;

    public Vector2 destination;

    Node[] Path;


    // Start is called before the first frame update
    void Start()
    {
        //test
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Path == null)
        {
            Path = Grid.FindPath(Grid.WorldToGridPosition(transform.position), Grid.WorldToGridPosition(Ending.transform.position));
        }
        //how to follow Vector3 array (Vector3 = path[x].WorldPosition)
        

        if (current < Path.Length)
        {
            //moves the position to the current waypoint, time.delta time is used to change the speed
            transform.position = Vector3.MoveTowards(transform.position, Path[current].WorldPosition(), Time.deltaTime * speed);

            if (Vector3.Distance(Path[current].WorldPosition(), transform.position) < WPradius)  //Checking distance of current waypoint and its position and the position of the current object and if its less than WPradius
            {
                current++;  // adds 1 to the current value
                if (current >= Path.Length) // basically checks the "[]" value therefore if it reaches its last waypoint it gets set back to 0
                {
                    //path = Grid.FindPath(path[path.Length - 1].Position, path[0].Position);
                    //current = 0;
                }
            }

            
        }


        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("Breakpoint");
        }
    }
}
