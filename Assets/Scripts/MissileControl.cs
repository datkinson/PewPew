using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileControl : MonoBehaviour
{
    public float lifetime;
    private float initialSpeed;
    public float maxSpeedDifference;
    public float force;
    public float damage;
    private Rigidbody2D missileRigidbody;
    public AnimationCurve forceCurve;
    private float age = 0;
    public GameObject ExplosionPrefab;
    private int parentID;
    // Start is called before the first frame update
    void Start()
    {
        missileRigidbody = GetComponent<Rigidbody2D>();
        initialSpeed = missileRigidbody.velocity.magnitude;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        age += Time.deltaTime;
        if (age > lifetime) { Destroy(gameObject); }
        var speedDifference = Mathf.Abs(missileRigidbody.velocity.magnitude - initialSpeed);
        if (speedDifference < maxSpeedDifference)
        {
            var curveValue = Mathf.Clamp01(forceCurve.Evaluate(age / lifetime));
            missileRigidbody.AddForce(new Vector2(missileRigidbody.transform.up.x, missileRigidbody.transform.up.y) * (curveValue * force));
        }
    }

    void HitObject(GameObject hitObject)
    {
        if (hitObject)
        {
            Instantiate(ExplosionPrefab, hitObject.transform.position, hitObject.transform.rotation);
            hitObject.GetComponent<DamageManager>().Hit(damage);
        }
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetInstanceID() != parentID)
        {
            HitObject(other.gameObject);
        }
    }

    public void SetParentID(int id)
    {
        parentID = id;
    }
}
