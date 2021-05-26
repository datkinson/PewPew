using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    public GameObject shotPrefab;
    public float rotationSpeed = 2;
    private Quaternion targetRotation;
    private float timeFired = 0;
    public const float cooldownTime = 1f;
    private Vector2 target;
    private bool fire;
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            if (fire)
            {
                ShootAt(target);
            } else
            {
                RotateTowards(target);
            }
        }
    }

    public void SetTarget(Vector2 targetCoordinates)
    {
        target = targetCoordinates;
    }

    public void EnableTrigger(bool trigger)
    {
        fire = trigger;
    }

    public void RotateTowards(Vector2 coordinates)
    {
        var angle = Mathf.Atan2(coordinates.y - transform.position.y, coordinates.x - transform.position.x) * Mathf.Rad2Deg - 90;
        targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public void ShootAt(Vector2 target) {
        RotateTowards(target);
        Vector3 targetPosition = new Vector3(target.x, target.y, 0);
        float angleToTarg = Vector3.Angle(transform.up, targetPosition - transform.position);
        if (angleToTarg < 10 && angleToTarg > -10)
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        if (Time.time - timeFired > cooldownTime)
        {
            var spawnedMissile = Instantiate(shotPrefab, transform.position, transform.rotation);
            spawnedMissile.GetComponent<MissileControl>().lifetime = 1;
            spawnedMissile.GetComponent<MissileControl>().SetParentID(GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>().GetInstanceID());
            var missileRigidbody = spawnedMissile.GetComponent<Rigidbody2D>();
            var shipRigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
            missileRigidbody.velocity = shipRigidbody.velocity;
            timeFired = Time.time;
        }
    }

}
