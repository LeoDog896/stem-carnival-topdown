using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackHitBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            var parent = collision.transform.parent;
            collision.GetComponent<EnemyHealthTracking>().TakeDamage(1, /* hit direction */ (collision.transform.position - parent.position));
        }
    }
}
