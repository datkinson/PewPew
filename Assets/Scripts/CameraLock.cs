using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLock : MonoBehaviour
{
    private Transform Camera;
    public Transform Ship;
    public Vector3 CameraOffset;
    // Start is called before the first frame update
    void Start()
    {
        Camera = transform;
    }

    // Update is called once per frame
    void Update()
    {
        Camera.position = Ship.position + CameraOffset;
    }
}
