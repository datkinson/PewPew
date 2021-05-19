using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private float shipParts = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addParts(float amount)
    {
        shipParts += amount;
    }

    public float getParts()
    {
        return shipParts;
    }
}
