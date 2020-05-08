using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    [SerializeField] List<Transform> waypoints;
    [SerializeField] WaveConfig waveConfig;
    [SerializeField] bool isDoneSpawning = false;
    private bool isBeingKnockedBack = false;
    private float thrust = 100f;
    float minDistance = 10;
    float moveSpeed;
    int waypointIndex = 0;
    Rigidbody2D rigidBody;
    private float totalDeltaTime;
    [SerializeField] float maxPathingTime = 3;


    // Start is called before the first frame update
    void Start()
    {

        float wepDistance = gameObject.GetComponent<AttackWithWeapon>().equippedWeapon.range;
        minDistance = wepDistance;

        if (moveSpeed <= 0)
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
        totalDeltaTime += Time.deltaTime;
        totalDeltaTime = totalDeltaTime % 10000; // Just making sure the number doesn't get stupid
        if (waypointIndex <= waypoints.Count - 1 && totalDeltaTime < maxPathingTime)
        {
            rigidBody.velocity = Vector3.zero;
            rigidBody.angularVelocity = 0f;
            var targetPosition = waypoints[waypointIndex].transform.position;
            var movementThisFrame = moveSpeed * Time.fixedDeltaTime;
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
            //rigidBody.velocity = Vector3.zero;
            //rigidBody.angularVelocity = 0f;
            Vector2 targetPosition = player.transform.position;
            if ((targetPosition - (Vector2)gameObject.transform.position).sqrMagnitude > minDistance)
            {
                var movementThisFrame = moveSpeed * Time.fixedDeltaTime;
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
            Debug.Log("No Player Found in enemy pathing script");
        }
    }


    public IEnumerator Knockback()
    {
        //Debug.Log("Enemy Knockback test");
        if (isBeingKnockedBack)
            yield break;

        isBeingKnockedBack = true;
        //var knockbackThisFrame = thrust * Time.fixedDeltaTime;
        //Vector2 knockbackTarget = new Vector2(gameObject.transform.position.x + 40, gameObject.transform.position.y + 40);
        //Vector2 knockback;
        //knockback = Vector2.MoveTowards(transform.position, knockbackTarget, knockbackThisFrame);
        //rigidBody.MovePosition(knockback);

        /////////////////////////////////

        //rigidBody.AddForce(new Vector2(gameObject.transform.position.x + 40, gameObject.transform.position.y + 40), ForceMode2D.Impulse);
        rigidBody.AddForce(Vector3.forward * -2000f, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.3f);
        //rigidBody.velocity = transform.forward * -200f;
        rigidBody.velocity = Vector3.zero;

        isBeingKnockedBack = false;
    }

}
