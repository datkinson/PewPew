using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float lifetime;
    private float age = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        age += Time.deltaTime;
        if (age > lifetime) { Destroy(gameObject); }
    }
}
