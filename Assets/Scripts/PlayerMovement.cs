using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Collider2D cd;
    [SerializeField] public float screenWidthInUnits = 5.4f;
    [SerializeField] public float screenHeightInUnits = 9.6f;
    [SerializeField] public float playerSpeed = 5f;
    private Vector3 destination = new Vector2(0, 0);
    private Vector3 tempVect;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        cd = gameObject.GetComponent<Collider2D>();
        //playerSpeed *= Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log("Destination: " + destination);
            tempVect = destination - transform.position;
            Debug.Log("TempVect: " + tempVect);
        }
        Move();
    }

    private void Move()
    {
        //transform.position = Vector2.MoveTowards(transform.position, destination, playerSpeed);
        rb.MovePosition(rb.transform.position + tempVect * (playerSpeed * Time.deltaTime));
        
        //rb.MovePosition(destination.normalized + tempVect);
        Debug.Log("MovePosition: " + (rb.transform.position + tempVect));
    }
}