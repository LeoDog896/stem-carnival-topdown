using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRangeFollow : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private Rigidbody2D rigidBody;
    public float speed = 3f;
    public float stopDistance = 3f;

    public float rotateSpeed = 0.25f;
    public Transform target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void RotateTowardsTarget()
    {
        Vector2 targetDirection = target.position - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 270f;
        Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.localRotation = Quaternion.Slerp(transform.localRotation, q, rotateSpeed);
    }
    private void FixedUpdate()
    {
        RotateTowardsTarget();
        rigidBody.AddForce((target.position - transform.position).normalized * speed);
    }

}
