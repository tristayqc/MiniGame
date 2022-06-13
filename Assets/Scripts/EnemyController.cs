using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float speed;
    private Rigidbody rb;
    private Transform target;
    Vector3 moveDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").transform;
        // could be null if no such gameobj found
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target) // not null
        {
            moveDirection = (target.position - transform.position).normalized; // unit vector
            rb.AddForce(moveDirection * speed);
            //rb.velocity = new Vector3(moveDirection.x, 0.0f, moveDirection.z) * speed;
        }
    }
}
