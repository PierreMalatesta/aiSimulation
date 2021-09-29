using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    public State currentState;

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
        if (currentState == State.Wander)
        {
            //Wander code
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
