using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineController : MonoBehaviour
{
    public float proximityFuse;
    public float explosionRange;
    public float damage;
    private float timeElapsed = 0;
    public float minimumAge;
    private float initializationTime;
    private GameObject player;
    public GameObject ExplosionPrefab;
    public GameObject rangeRing;
    public GameObject[] countdownSprites;
    private float fuseTimer;
    private float detectionRange;
    private bool fuseTriggered = false;

    // Start is called before the first frame update
    void Start()
    {
        //fuseTimer = player.GetComponent<WeaponControl>().fuseTimer;
        //detectionRange = player.GetComponent<WeaponControl>().detectionRange;
    }

    private void Update()
    {
        rangeRing.transform.localScale = new Vector3(detectionRange / 3, detectionRange / 3, 1);
        if (fuseTriggered)
        {
            int timeLeft = (int)fuseTimer - (int)timeElapsed;
            if(countdownSprites.Length > 0 && timeLeft < countdownSprites.Length)
            {
                for (int i = 0; i < countdownSprites.Length; i++)
                {
                    GameObject countdownSprtie = countdownSprites[i];
                    if(i == timeLeft)
                    {
                        countdownSprtie.SetActive(true);
                    } else
                    {
                        countdownSprtie.SetActive(false);
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (fuseTriggered)
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed > fuseTimer) {
                Detonate();
            }
        }
        List<GameObject> targets = GetEnemiesInRange(explosionRange);
        if(targets.Count > 0)
        {
            StartFuseTimer();
        }
    }

    public void Detonate()
    {
        if(Time.timeSinceLevelLoad - initializationTime > minimumAge)
        {
            Instantiate(ExplosionPrefab, transform.position, transform.rotation);
            List<GameObject> targets = GetEnemiesInRange(explosionRange);
            foreach(GameObject target in targets)
            {
                target.GetComponent<DamageManager>().Hit(damage);
            }
            Destroy(gameObject);
        }
    }

    List<GameObject> GetEnemiesInRange(float range)
    {
        List<GameObject> enemiesInRange = new List<GameObject>();
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject target in enemies)
        {
            float distance = Vector3.Distance(target.transform.position, transform.position);
            if (distance < range)
            {
                enemiesInRange.Add(target);
            }
        }
        return enemiesInRange;
    }

    void StartFuseTimer()
    {
        fuseTriggered = true;
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
