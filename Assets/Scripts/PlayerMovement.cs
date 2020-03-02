using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float screenWidthInUnits = 18f;
    [SerializeField] float screenHeightInUnits = 32f;
    [SerializeField ]public float speed = 25f;
    private Vector2 destination = new Vector2(9, 16);

    // Start is called before the first frame update
    void Start()
    {
        speed *= Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            float mouseXPosInUnits = Input.mousePosition.x / Screen.width * screenWidthInUnits;
            float mouseYPosInUnits = Input.mousePosition.y / Screen.height * screenHeightInUnits;
            destination = new Vector2(mouseXPosInUnits, mouseYPosInUnits);
        }
        Move();
    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, destination, speed);
    }
}
