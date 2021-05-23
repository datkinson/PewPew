using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControl : MonoBehaviour
{
    public GameObject MissilePrefab;
    public GameObject MinePrefab;
    private float timeFired = 0;
    public float cooldownTime = 3;
    
    public float fuseTimer;
    public float detectionRange;
    private GameObject mine;

    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("FireMissile") > 0)
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
        if (Input.GetButtonDown("Mine"))
        {
            if (mine)
            {
                mine.GetComponent<MineController>().Detonate();
            }
            else
            {
                mine = Instantiate(MinePrefab, transform.position, Quaternion.Euler(new Vector3(0,0,0)));
                var shipRigidbody = GetComponent<Rigidbody2D>();
                var mineRigidbody = mine.GetComponent<Rigidbody2D>();
                mineRigidbody.velocity = shipRigidbody.velocity;
                mine.GetComponent<MineController>().UpdateFuseTimer(fuseTimer);
                mine.GetComponent<MineController>().UpdateDetectionRange(detectionRange);
            }
        }
    }

    public void UpdateFuseTimer(float timerValue)
    {
        fuseTimer = timerValue;
    }

    public void UpdateDetectionRange(float rangeValue)
    {
        detectionRange = rangeValue;
    }
}
