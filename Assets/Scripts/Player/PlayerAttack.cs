using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public bool touching = false;

    public BoxCollider2D hitbox;


    // Start is called before the first frame update
    public void Start()
    {
    }

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Invoke("ActivateHitbox", 0.0f); // Activate hitbox after 0.1 seconds.
            Invoke("DeactivateHitbox", 0.3f); // Deactivate hitbox after 0.8 seconds.
        }
    }

    public void ActivateHitbox()
    {
        touching = true;
        hitbox.gameObject.GetComponent<Renderer>().material.color = new Color(255, 0, 0);
    }
    public void DeactivateHitbox()
    {
        touching = false;
        hitbox.gameObject.GetComponent<Renderer>().material.color = new Color(255, 255, 255);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.gameObject.tag == "Enemy")
        {
            if (touching){
                var tracker = collision.gameObject.GetComponent<EnemyHealthTracking>();
                if (tracker)
                {
                    tracker.TakeDamage(1, (collision.transform.position - transform.position).normalized);
                }
            }
        }
    }
}