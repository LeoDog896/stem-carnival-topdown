using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardThrow : MonoBehaviour
{ 
    public GameObject card;

    private float step;

    private Vector2 move;

    public float speed = 10f;

    private Transform target;

    private Rigidbody2D CardBody;
    // Start is called before the first frame update



    void Start()
    {
        CardBody = this.GetComponent<Rigidbody2D>();

        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();


        Invoke("Card", 0.1f);
    }

    // Update is called once per frame

    public void Card()
    {
        move = Vector2.MoveTowards(transform.position, target.position, step);
    }

    void Update()
    {
        card.transform.position = Vector2.MoveTowards(move, move, step);
    }
}
