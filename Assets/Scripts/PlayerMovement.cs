using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float playerSpeed = 9f;
    private bool isBeingKnockedBack = false;
    private float thrust = 2000f;
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
        if(!isBeingKnockedBack)
        {
            Move();
        }
    }

    private void Move()
    {
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = 0f;
        rigidBody.MovePosition(Vector3.MoveTowards(transform.position, destination, playerSpeed * Time.fixedDeltaTime));
    }

    public IEnumerator Knockback()
    {
        if (isBeingKnockedBack)
            yield break;

        isBeingKnockedBack = true;
        rigidBody.AddForce(Vector3.forward * -thrust, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.4f);
        destination = transform.position;
        isBeingKnockedBack = false;
    }
}