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
    public float rotationSpeed = 3;
    public float scriptRotationSpeed = 50;
    public float previousYAxis;
    public float previousXAxis;
    private AudioSource thrusterSound;
    private bool thrustersActive = false;
    private Quaternion targetRotation;
    private bool autoRotate = false;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        thrusterSound = GetComponent<AudioSource>();
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
        Rotate(-xAxis * rotationSpeed);
        //mainThruster.enabled = (yAxis > 0);
        //reverseThruster.enabled = (yAxis < 0);
        //leftRCS.enabled = (xAxis < 0 || (xAxis == 0 && previousXAxis > 0));
        //rightRCS.enabled = (xAxis > 0 || (xAxis == 0 && previousXAxis < 0));
        //previousXAxis = xAxis;
        //previousYAxis = yAxis;
        if (Input.GetAxis("Brake") > 0) { Brake(); }
        var thrusterStates = new List<bool>()
        {
            mainThruster.enabled,
            reverseThruster.enabled,
            leftRCS.enabled,
            rightRCS.enabled
        };
        activeThrusterSound(thrusterStates.Contains(true));
    }

    private void ClampVelocity()
    {
        float x = Mathf.Clamp(rb.velocity.x, -maxVelocity, maxVelocity);
        float y = Mathf.Clamp(rb.velocity.y, -maxVelocity, maxVelocity);
        rb.velocity = new Vector2(x, y);
    }

    public void ThrustForward(float amount)
    {
        Vector2 force = transform.up * amount;
        rb.AddForce(force);
        mainThruster.enabled = (amount > 0);
        reverseThruster.enabled = (amount < 0);
    }

    public void Rotate(float amount)
    {
        autoRotate = false;
        rb.angularVelocity = 0;
        transform.Rotate(0, 0, amount);
        leftRCS.enabled = (amount < 0 || (amount == 0 && previousXAxis > 0));
        rightRCS.enabled = (amount > 0 || (amount == 0 && previousXAxis < 0));
        previousXAxis = amount;
        previousYAxis = amount;
    }

    public void Brake()
    {
        rb.AddForce(-rb.velocity);
        rb.angularVelocity = rb.angularVelocity - (rb.angularVelocity/ 50);
        mainThruster.enabled = true;
        reverseThruster.enabled = true;
        leftRCS.enabled = true;
        rightRCS.enabled = true;
    }

    public void RotateTowards(Vector2 coordinates)
    {
        autoRotate = true;
        var angle = Mathf.Atan2(coordinates.y - transform.position.y, coordinates.x - transform.position.x) * Mathf.Rad2Deg - 90;
        targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, scriptRotationSpeed * Time.deltaTime);
    }

    void activeThrusterSound(bool thrusterState)
    {
        if (thrusterState != thrustersActive)
        {
            thrustersActive = thrusterState;
            if (thrusterState)
            {
                thrusterSound.Play(0);
            } else
            {
                thrusterSound.Stop();
            }
        }
    }
}
