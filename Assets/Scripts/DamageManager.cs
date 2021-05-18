using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    public float hitPoints;
    public float shieldPoints;
    public SpriteRenderer[] shields;
    public AudioClip[] destroySound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (shields.Length > 0)
        {
            foreach (SpriteRenderer shield in shields)
            {
                shield.enabled = false;
            }
            if (shieldPoints > 0)
            {
                var shieldsToDisplay = (shields.Length < shieldPoints) ? shields.Length : (int)shieldPoints;
                shields[shieldsToDisplay - 1].enabled = true;
            }
        }
    }

    public void Hit(float damage)
    {
        if (shieldPoints > 0)
        {
            shieldPoints = shieldPoints - damage;
            //ShowShieldHit();
        } else
        {
            hitPoints = hitPoints - damage;
            if (hitPoints > 0)
            {
                ShowDamageHit();
            } else
            {
                DestroyShip();
            }
        }
    }

    /*void ShowShieldHit()
    {

    }*/

    void ShowDamageHit()
    {

    }

    void DestroyShip()
    {
        if (destroySound.Length > 0)
        {
            int index = Random.Range(0, destroySound.Length);
            AudioSource.PlayClipAtPoint(destroySound[index], gameObject.transform.position);
        }
        Destroy(gameObject);
    }
}
