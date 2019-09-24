using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    SpriteRenderer render;
    public Text textScore;

    int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector3 tempVect = new Vector3(moveHorizontal, 0, 0);
        tempVect = tempVect.normalized * speed * Time.deltaTime;
        rb.MovePosition(rb.transform.position + tempVect);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Goal" && render.color == new Color(238f / 255f, 53f / 255f, 224f / 255f))
        {
            render.color = new Color(255f / 255f, 107f / 255f, 93f / 255f);
            FindObjectOfType<PeopleSpawner>().Spawn();
            score++;

            textScore.text = score + "";
        }
    }
}
