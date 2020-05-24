using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public LayerMask enemylayer;
    public float damage = 1f;
    public float radius = 0.3f;

    private EnemyHealth enemyHealth;
    private bool collided;
   
    void Update()
    {
        CheckForDamage();
    }

    void CheckForDamage()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, enemylayer);

        foreach(Collider h in hits)
        {
            enemyHealth = h.GetComponent<EnemyHealth>();
            if (enemyHealth)
            {
                collided = true;
            }
        }

        if (collided)
        {
            collided = false;
            enemyHealth.TakeDamage(damage);
            gameObject.SetActive(false);

        }
    }
}
