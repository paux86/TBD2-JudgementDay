using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    [SerializeField] List<Transform> waypoints;
    [SerializeField] WaveConfig waveConfig;
    [SerializeField] bool isStatic = false;
    float moveSpeed;
    int waypointIndex = 0;
    Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        if(!isStatic)
        {
            moveSpeed = waveConfig.GetMoveSpeed();
            waypoints = waveConfig.GetWayPoints();
            transform.position = waypoints[waypointIndex].transform.position;
        }

        rigidBody =  gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

       if(!isStatic)
        {
            Move();
        }
    }


    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }
    private void Move()
    {
        if (waypointIndex <= waypoints.Count - 1)
        {
            var targetPosition = waypoints[waypointIndex].transform.position;
            var movementThisFrame = moveSpeed * Time.deltaTime;
            Vector2 moveMent;
            moveMent = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);
            rigidBody.MovePosition(moveMent);




            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }

        }
        
    }
}
