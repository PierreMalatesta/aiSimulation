using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAI : MonoBehaviour
{
    public Vector2 start;
    public Vector2 destination;
    public float moveSpeed;

    private int x;

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

        transform.position = Vector3.MoveTowards(transform.position, path[x].WorldPosition);
    }
}
