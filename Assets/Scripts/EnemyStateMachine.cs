using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    public State currentState;
    public Transform[] wanderNodes;

    public Collider playerCollider;

    public float WPradius;
    public float speed;

    public bool mightDeactivate;

    int currentWanderNode;

    Node[] followNodes;


    int currentFollowNode;

    Transform Player;

    public GameObject panel;

    //easiest way to state machines
    //do this for enemies not player.
    public enum State
    {
        Follow,
        Wander,
        Attack
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        speed -= Random.Range(0, 2f);
        panel.SetActive(false);
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

        if (currentState == State.Follow)
        {
            if (followNodes != null && currentFollowNode < followNodes.Length)
            {
                transform.position = Vector3.MoveTowards(transform.position, followNodes[currentFollowNode].WorldPosition(), Time.deltaTime * speed);

                if (Vector3.Distance(followNodes[currentFollowNode].WorldPosition(), transform.position) < WPradius)
                {
                    currentFollowNode++;
                    if (currentFollowNode > 2) SetPathToPlayer();
                    else if (currentFollowNode >= followNodes.Length)
                    {
                        currentFollowNode = 0;
                        SetPathToPlayer();
                    }
                    
                }
            }

            //once very close to player
            if (Vector3.Distance(transform.position, Player.position) < 1f)
            {
                Time.timeScale = 0;
                //turn on gameover screen
                panel.SetActive(true);
            }
            //transform.rotation = Quaternion.Slerp(transform.rotation
            //                                , Quaternion.LookRotation(Player.position - transform.position)
            //                                , RotSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (currentState != State.Follow)
        {
            if (other.tag == "Player")
            {
                //change state to follow
                //get grid position of this enemy use Grid.WorldToGridPosition()
                //get grid position of player
                //pathfind Grid.PathFind()
                currentState = State.Follow;

                SetPathToPlayer();

            }
        }

    }

   

    void SetPathToPlayer()
    {
        Vector2 Enemy = Grid.WorldToGridPosition(transform.position);
        Vector2 playerGrid = Grid.WorldToGridPosition(Player.position);


        followNodes = Grid.FindPath(Enemy, playerGrid);
        currentFollowNode = 0;
        Debug.Log("nothing");
    }

}
