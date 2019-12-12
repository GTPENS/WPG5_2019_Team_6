using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float speed;
    private float waitTime;
    public float startWaitTime;

    public Transform enemySpot;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    public Transform player;
    public float chaseRange;

    public int position;


    // Start is called before the first frame update
    void Start()
    {
        waitTime = startWaitTime;

        enemySpot.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, enemySpot.position, speed * Time.deltaTime);

        if(Vector2.Distance(transform.position, enemySpot.position) < 0.2f)
        {
            if(waitTime <= 0)
            {
                enemySpot.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                waitTime = startWaitTime;
            } else
            {
                waitTime -= Time.deltaTime;
            }
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer < chaseRange)
        {
            Vector3 playerDir = player.position - transform.position;
            float angle = Mathf.Atan2(playerDir.y, playerDir.x) * Mathf.Rad2Deg - 90f;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 360);

            transform.Translate(Vector3.up * Time.deltaTime * speed);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = FindObjectOfType<Player>();

        //FindObjectOfType<SpawnManager>().PickUp(position);

        //SpriteRenderer p = player.GetComponent<SpriteRenderer>();
    
       player.speed = 9;

        Destroy(this.gameObject);
    }
}
