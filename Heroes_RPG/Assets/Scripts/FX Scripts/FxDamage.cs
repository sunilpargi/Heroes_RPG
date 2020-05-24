using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxDamage : MonoBehaviour
{
    public LayerMask enemylayer;
    public float damage = 20f;
    public float radius = 2f;

    private EnemyHealth enemyHealth;
    private bool collided;

    void Update()
    {
        CheckForDamage();
    }

    void CheckForDamage()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, enemylayer);

        foreach (Collider h in hits)
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
