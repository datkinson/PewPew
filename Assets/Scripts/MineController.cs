using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineController : MonoBehaviour
{
    public float proximityFuse;
    public float fuseTimer;
    public float detectionRange;
    public float explosionRange;
    private float timeElapsed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // detect if any enemies are in detectionRange
        // start timer
        // if timer elapsed then explode
        // check if any enemies are in explosionRange
        // damage any enemies in range
    }
}
