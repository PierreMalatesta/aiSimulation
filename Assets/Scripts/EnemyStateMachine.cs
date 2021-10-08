using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    public State currentState;
    public Transform[] wanderNodes;
    
    public float WPradius;
    public float speed;

    int currentWanderNode;

    //easiest way to state machines
    //do this for enemies not player.
    public enum State
    {
        Follow,
        Wander,
        Flee
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == State.Wander)
        {
            //Wander code
            if (currentWanderNode < wanderNodes.Length)
            {
                //moves the position to the current waypoint, time.delta time is used to change the speed
                transform.position = Vector3.MoveTowards(transform.position, wanderNodes[currentWanderNode].position, Time.deltaTime * speed);

                if (Vector3.Distance(wanderNodes[currentWanderNode].position, transform.position) < WPradius)  //Checking distance of current waypoint and its position and the position of the current object and if its less than WPradius
                {
                    currentWanderNode++;  // adds 1 to the current value
                    if (currentWanderNode >= wanderNodes.Length) // basically checks the "[]" value therefore if it reaches its last waypoint it gets set back to 0
                    {
                        //path = Grid.FindPath(path[path.Length - 1].Position, path[0].Position);
                        currentWanderNode = 0;
                    }
                }


            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (currentState ==  State.Follow)
        {
            if (other.CompareTag("Player"))
            {
                //change state to follow
                //get grid position of this enemy use Grid.WorldToGridPosition()
                //get grid position of player
                //pathfind Grid.PathFind()

                other = Grid.FindPath(start, Grid.WorldToGridPosition(Ending.transform.position));
            }
        }
    }
}
