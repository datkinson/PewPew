using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileControl : MonoBehaviour
{
    public float lifetime;
    private float initialSpeed;
    public float maxSpeedDifference;
    public float force;
    private Rigidbody2D missileRigidbody;
    public AnimationCurve forceCurve;
    private float age = 0;
    public GameObject ExplosionPrefab;
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
        Instantiate(ExplosionPrefab, hitObject.transform.position, hitObject.transform.rotation);
        Destroy(hitObject);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        HitObject(other.gameObject);
    }
}
