using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    public Transform[] spawnPoint;

    // Start is called before the first frame update
    void Awake()
    {

        transform.position = spawnPoint[Random.Range(0, spawnPoint.Length)].position;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
