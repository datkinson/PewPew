using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControl : MonoBehaviour
{
    public GameObject MissilePrefab;
    private float timeFired = 0;
    public float cooldownTime = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("FireMissile") > 0) {
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
}
