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
    public int amount = 0;
    public Animation anim;

    int score = 0;
    bool isFacingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animation>();
        rb = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if((moveHorizontal < 0f && isFacingRight) || (moveHorizontal > 0f && !isFacingRight))
        {
            isFacingRight = !isFacingRight;
            Vector3 temp = transform.localScale;
            temp.x *= -1;
            transform.localScale = temp;
        }

        Vector3 tempVect = new Vector3(moveHorizontal, moveVertical, 0);
        tempVect = tempVect.normalized * speed * Time.deltaTime;
        rb.MovePosition(rb.transform.position + tempVect);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Goal" && amount > 0)
        {
            render.color = new Color(255f / 255f, 107f / 255f, 93f / 255f);
            FindObjectOfType<SpawnManager>().SpawnAll();
            score += amount;
            amount = 0;

            textScore.text = score + "";
        }
    }
}
