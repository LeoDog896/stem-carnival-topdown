using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectTransformFollow : MonoBehaviour
{
    // The Transform to follow; we'll only copy its position
    [SerializeField] private Transform targetTransform;
    // The offset from the target's position
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - targetTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = targetTransform.position + offset;
    }
}
