using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFollow : MonoBehaviour
{
    [Header("Movement")]
    private Rigidbody2D rb;
    public float radius; // for ranged enegmies
    public float speed = 3f;

    private float canAttack;
    private int mobType_;

    public GameObject EnemyType_;

    public float rotateSpeed = 0.25f;

    public Transform target;

    private float step;

    private bool hit = false;

    public SpriteRenderer colorchanger;

    private int hmc = 1; //funny feature to change color of hurt color with less hp, does not work for now :/

    public float health;
    [SerializeField] private float maxHealth;


    void Start()
    {
        colorchanger = GetComponent<SpriteRenderer>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(float dmg)
    {
        if (!hit)
        {
            GetTarget();
            var playerPosition = target.position;
            var force = (playerPosition - this.transform.position).normalized * new Vector2(-500f, -500f);
            Debug.Log(force);
            this.GetComponent<Rigidbody2D>().AddForce(force);
            colorchanger.color = new Color(255, 0, 0);
            health -= dmg;
            hit = true;
            Invoke("cooldown", 0.1f);
        }

        if (health <= 0)
        {
            health = 0;
            Destroy(gameObject);
        }
    }

   
    public void EnemyType(int mob, GameObject test)
    {
        mobType_ = mob;
        //EnemyType_ = test.FindGameObjectWithTag("Enemy").GetComponent<AIFollow>();

        Debug.Log(mobType_);

        if (mob == 0) //card
        {
            maxHealth = 1;
            health = maxHealth;
            speed = 4.5f;
        } else if (mob == 1) //brawler
        {
            maxHealth = 3;
            health = maxHealth;
            speed = 2.5f;
        }
    }
    public void cooldown()
    {
        Invoke("damageable", 0.3f);
    }

    public void damageable()
    {
        colorchanger.color = new Color(255, 255, 255);
        hit = false;
    }

    private void RotateTowardsTarget()
    {
        Vector2 targetDirection = target.position - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 270f;
        Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.localRotation = Quaternion.Slerp(transform.localRotation, q, rotateSpeed);
    }
    private void Update()
    {
        if (target != null)
        {
            radius = (target.transform.position - transform.position).magnitude;

            step = speed * Time.deltaTime;

            if (radius <= 5 && this.mobType_ == 0)
            {
               // step = 0;
            }

            RotateTowardsTarget();
            
            transform.position = Vector2.MoveTowards(transform.position, target.position, step);
        }
        else
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }
    
    private void GetTarget()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

}
