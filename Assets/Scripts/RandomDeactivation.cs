using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDeactivation : MonoBehaviour
{

    public bool mightDeactivate;

// Start is called before the first frame update
void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (mightDeactivate && Random.Range(0, 1f) > 0.5f)
        {
            gameObject.SetActive(false);
            return;
        }
    }
}
