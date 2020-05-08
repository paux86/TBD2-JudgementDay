using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float playerSpeed = 9f;
    private bool isBeingKnockedBack = true;
    private float thrust = 1000f;
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
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
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

    public IEnumerator Knockback()
    {
        Debug.Log("Knockback test - Player");
        if (isBeingKnockedBack)
            yield break;

        isBeingKnockedBack = true;
        Vector2 knockback = new Vector2(2f, 1f);
        rigidBody.velocity = transform.right * thrust;
        //rigidBody.AddForce(knockback * thrust * Time.deltaTime, ForceMode2D.Impulse);
        //rigidBody.AddForce(knockback * thrust * Time.deltaTime);
        //yield return new WaitForSeconds(1.0f);
        isBeingKnockedBack = false;
    }
}