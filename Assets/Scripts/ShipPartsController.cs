using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPartsController : MonoBehaviour
{
    public GameObject player;
    public float points;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetInstanceID() == player.GetComponent<BoxCollider2D>().GetInstanceID())
        {
            player.GetComponent<InventoryController>().addParts(points);
            Destroy(gameObject);
        }
    }
}
