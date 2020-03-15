using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float playerSpeed = 9f;
    private Vector3 destination;
    private Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        destination = transform.position;
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            destination.z = transform.position.z;
        }
        Move();
    }

    private void Move()
    {
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = 0f;
        rigidBody.MovePosition(Vector3.MoveTowards(transform.position, destination, playerSpeed * Time.fixedDeltaTime));
    }
}