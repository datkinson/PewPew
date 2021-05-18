using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public float rotationSpeed = 2;
    public float maxVelocity = 1;
    public int frictionFactor = 1;
    public float accuracyDeg;
    private float timeFired = 0;
    private const float cooldownTime = 3;
    private Rigidbody2D targetObject;
    private Rigidbody2D enemy;
    private Vector3 targetPoint;
    private Quaternion targetRotation;
    public SpriteRenderer forwardThrusterLeft;
    public SpriteRenderer forwardThrusterRight;
    public GameObject MissilePrefab;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Rigidbody2D>();
        forwardThrusterLeft.enabled = false;
        forwardThrusterRight.enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (targetObject)
        {
            var angle = Mathf.Atan2(targetObject.transform.position.y - transform.position.y, targetObject.transform.position.x - transform.position.x) * Mathf.Rad2Deg - 90;
            targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            ThrustForward(1);
            float angleToTarg = Vector3.Angle(transform.up, targetObject.transform.position - transform.position);
            if (angleToTarg < 10 && angleToTarg > -10)
            {
                Shoot();
            }
        } else
        {
            //GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Brake();
        }
    }

    public void SetTarget(Rigidbody2D target)
    {
        targetObject = target;
    }

    public void UnsetTarget()
    {
        targetObject = null;
    }

    private void ThrustForward(float amount)
    {
        Vector2 force = transform.up * amount;
        GetComponent<Rigidbody2D>().AddForce(force);
        forwardThrusterLeft.enabled = true;
        forwardThrusterRight.enabled = true;
    }
    private void Rotate(Transform t, float amount)
    {
        t.Rotate(0, 0, amount);
    }

    void Brake()
    {
        enemy.AddForce(-enemy.velocity * frictionFactor);
        forwardThrusterLeft.enabled = false;
        forwardThrusterRight.enabled = false;
    }

    void Shoot()
    {
        if (Time.time - timeFired > cooldownTime)
        {
            var spawnedMissile = Instantiate(MissilePrefab, transform.position, transform.rotation);
            spawnedMissile.GetComponent<MissileControl>().SetParentID(GetComponent<BoxCollider2D>().GetInstanceID());
            var missileRigidbody = spawnedMissile.GetComponent<Rigidbody2D>();
            var shipRigidbody = GetComponent<Rigidbody2D>();
            missileRigidbody.velocity = shipRigidbody.velocity;
            timeFired = Time.time;
        }
    }
}
