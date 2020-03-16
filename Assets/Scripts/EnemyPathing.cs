using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    [SerializeField] List<Transform> waypoints;
    [SerializeField] WaveConfig waveConfig;
    [SerializeField] bool isDoneSpawning = false;
    [SerializeField] float minDistance = 10;
    float moveSpeed;
    int waypointIndex = 0;
    Rigidbody2D rigidBody;
    private int frames;
    const int MAX_FRAMES = 200;


    // Start is called before the first frame update
    void Start()
    {

        float wepDistance = gameObject.GetComponent<AttackWithWeapon>().equippedWeapon.range;
        if (minDistance > wepDistance)
        {
            minDistance = wepDistance;
        }

        if(moveSpeed <= 0)
        {
            moveSpeed = GetComponent<Enemy>().GetMoveSpeed();
        }

        if (!isDoneSpawning && waveConfig != null)
        {
            moveSpeed = waveConfig.GetMoveSpeed();
            waypoints = waveConfig.GetWayPoints();
            transform.position = waypoints[waypointIndex].transform.position;
        }
        else
        {
            isDoneSpawning = true;
            
        }

        rigidBody =  gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

       if(!isDoneSpawning)
        {
            Move();
        }
       else
        {
            MoveTowardsPlayer();
        }
    }


    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }
    private void Move()
    {
        frames++;
        frames = frames % 10000; // Just making sure the number doesn't get stupid
        if (waypointIndex <= waypoints.Count - 1 && frames < MAX_FRAMES)
        {
            rigidBody.velocity = Vector3.zero;
            rigidBody.angularVelocity = 0f;
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
        else
        {
            isDoneSpawning = true;
        }


    }

    private void MoveTowardsPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player)
        {
            rigidBody.velocity = Vector3.zero;
            rigidBody.angularVelocity = 0f;
            Vector2 targetPosition = player.transform.position;
            if ((targetPosition - (Vector2)gameObject.transform.position).sqrMagnitude > minDistance)
            {
                var movementThisFrame = moveSpeed * Time.deltaTime;
                Vector2 moveMent;
                moveMent = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);
                rigidBody.MovePosition(moveMent);
            }
            Vector2 lookDir = targetPosition - rigidBody.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            rigidBody.rotation = angle;

        }
        else
        {
            Debug.Log("No Player Found");
        }
    }

}
