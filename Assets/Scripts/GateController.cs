using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    public GameObject player;
    public Canvas gameCanvas;
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
            gameCanvas.GetComponent<GameCanvasController>().EnterGate(player.GetComponent<InventoryController>().getParts());
        }
    }
}
