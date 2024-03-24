using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class AIRangeFollow : MonoBehaviour
{
    [Header("Movement")]
    private Rigidbody2D rb;
    public float radius; // for ranged enegmies
    public float speed = 3f;

    private float canAttack;
    private int mobType_;

    public GameObject EnemyType_;

    public float rotateSpeed = 0.25f;

    public int CardLimit;


    public Transform target;

    private float step;


    private bool hit = false;

    public SpriteRenderer colorchanger;


    public GameObject card;

    private int hmc = 1; //funny feature to change color of hurt color with less hp, does not work for now :/

    public float health;
    [SerializeField] private float maxHealth;


    void Start()
    {
        CardLimit = 0;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        card.transform.position = transform.position;
    }

    public void TakeDamage(float dmg)
    {
        if (!hit)
        {
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

        if (mob == 0) //card
        {
            maxHealth = 1;
            health = maxHealth;
            speed = 4.5f;
        }
        else if (mob == 1) //brawler
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

            RotateTowardsTarget();

            if (radius >= 5)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, step);
            }
            else
            {
                Invoke("CardThrow", 0.5f);
            }
        }
        else
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    private void CardThrow()
    {
        if(CardLimit < 1)
        {
            CardLimit = CardLimit + 1;
            GameObject obj = Instantiate(card, transform.position, Quaternion.identity);
            MoveToPlayer(obj);
        }
        else
        {
        }
    }


private void GetTarget()
{
    if (GameObject.FindGameObjectWithTag("Player"))
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

}

    private void MoveToPlayer(GameObject obj)
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        bool notTouching = false;
        while (!notTouching)
        {
            obj.transform.position = Vector2.MoveTowards(transform.position, target.position, step);
        }
    }
}
