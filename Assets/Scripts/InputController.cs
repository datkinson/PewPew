using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private Rigidbody2D rb;
    public SpriteRenderer mainThruster;
    public SpriteRenderer leftRCS;
    public SpriteRenderer rightRCS;
    public SpriteRenderer reverseThruster;
    public float maxVelocity = 3;
    public float rotationSpeed = 2;
    public float previousYAxis;
    public float previousXAxis;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float yAxis = Input.GetAxis("Vertical");
        float xAxis = Input.GetAxis("Horizontal");

        // reset thrusters
        mainThruster.enabled = false;
        reverseThruster.enabled = false;
        leftRCS.enabled = false;
        rightRCS.enabled = false;


        ThrustForward(yAxis);
        Rotate(transform, -xAxis * rotationSpeed);
        mainThruster.enabled = (yAxis > 0);
        reverseThruster.enabled = (yAxis < 0);
        leftRCS.enabled = (xAxis < 0 || (xAxis == 0 && previousXAxis > 0));
        rightRCS.enabled = (xAxis > 0 || (xAxis == 0 && previousXAxis < 0));
        previousXAxis = xAxis;
        previousYAxis = yAxis;
        if (Input.GetAxis("Brake") > 0) { Brake(); }
    }

    private void ClampVelocity()
    {
        float x = Mathf.Clamp(rb.velocity.x, -maxVelocity, maxVelocity);
        float y = Mathf.Clamp(rb.velocity.y, -maxVelocity, maxVelocity);
        rb.velocity = new Vector2(x, y);
    }

    private void ThrustForward(float amount)
    {
        Vector2 force = transform.up * amount;
        rb.AddForce(force);
    }

    private void Rotate(Transform t, float amount)
    {
        rb.angularVelocity = 0;
        t.Rotate(0, 0, amount);
    }

    void Brake()
    {
        rb.AddForce(-rb.velocity);
        rb.angularVelocity = rb.angularVelocity - (rb.angularVelocity/ 50);
        Debug.Log(rb.angularVelocity);
        mainThruster.enabled = true;
        reverseThruster.enabled = true;
        leftRCS.enabled = true;
        rightRCS.enabled = true;
        
    }
}