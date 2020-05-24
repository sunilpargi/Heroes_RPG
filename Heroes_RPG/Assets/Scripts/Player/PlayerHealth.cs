using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;

    public GameObject deathFx;

    public void TakeDamage(float damage)
    {
        health -= damage;

        print("damge received");
        if (health <= 0)
        {
            Instantiate(deathFx, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
