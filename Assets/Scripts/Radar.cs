using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    private GameObject target;
    private Rigidbody2D shipRigidbody;
    private Rigidbody2D targetRigidbody;
    public float range;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player");
        shipRigidbody = GetComponent<Rigidbody2D>();
        targetRigidbody = target.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var xDifference = Mathf.Abs(shipRigidbody.position.x - targetRigidbody.position.x);
        var yDifference = Mathf.Abs(shipRigidbody.position.y - targetRigidbody.position.y);
        if (xDifference < range && yDifference < range)
        {
            GetComponent<EnemyControl>().SetTarget(targetRigidbody);
        } else
        {
            GetComponent<EnemyControl>().UnsetTarget();
        }
    }
}
