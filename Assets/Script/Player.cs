using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;

public class Player : Photon.MonoBehaviour, IPunObservable
{
    Rigidbody2D rb;
    public float speed;
    SpriteRenderer render;
    public Text textScore;
    public Text enemyScore;
    public int amount = 0;
    public bool isDead = false;
    Animator anim;
    float dirX;

    int score = 0;
    bool isFacingRight = true;

    private PhotonView photonView;

    Vector3 curLocalScale;
    Vector3 targetPos;

    private void Awake() {
        photonView = GetComponent<PhotonView>();
        photonView.ObservedComponents.Add(this);
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();

        NetworkManager.instance.InitAll(this);

        if (!photonView.isMine)
            return;

        NetworkManager.instance.InitMine(this);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!photonView.isMine) {
            if (curLocalScale != null)
                transform.localScale = curLocalScale;
            if (targetPos != null)
                transform.position = Vector3.Lerp(transform.position, targetPos, 0.25f);

            return;
        }
        else if (isDead)
            return;

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if((moveHorizontal < 0f && isFacingRight) || (moveHorizontal > 0f && !isFacingRight))
        {
            isFacingRight = !isFacingRight;
            Vector3 temp = transform.localScale;
            temp.x *= -1;
            transform.localScale = temp;

            curLocalScale = transform.localScale;
        }

        if ((moveHorizontal != 0 || moveVertical != 0) && !anim.GetCurrentAnimatorStateInfo(0).IsName("HackAnim")) {
            anim.SetBool("isWalking", true);
        }
        else {
            anim.SetBool("isWalking", false);
        }

        Vector3 tempVect = new Vector3(moveHorizontal, moveVertical, 0);
        tempVect = tempVect.normalized * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.transform.position + tempVect);
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.isWriting) {
            if (photonView.isMine) {
                stream.SendNext(transform.localScale);
                stream.SendNext(transform.position);
            }
        }
        else {
            if (!photonView.isMine) {
                curLocalScale = (Vector3)stream.ReceiveNext();
                targetPos = (Vector3)stream.ReceiveNext();
            }
        }
    }

    /*private void Update()
    {
        if (!photonView.isMine)
            return;
           
        dirX = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;

        transform.position = new Vector2(transform.position.x + dirX, transform.position.y);

        if (dirX != 0 && !anim.GetCurrentAnimatorStateInfo(0).IsName("HackAnim"))
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
    }*/

    public void End() {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Goal" && amount > 0)
        {
            render.color = new Color(255f / 255f, 107f / 255f, 93f / 255f);
            FindObjectOfType<SpawnManager>().SpawnAll();
            score += amount;
            amount = 0;

            if (photonView.isMine) {
                print("SENT SCORE: " + score);

                textScore.text = score + "";

                //Sent to enemy
                photonView.RPC("ScoreMessage", PhotonTargets.Others, score);
            }
        }
    }

    [PunRPC]
    void ScoreMessage(int enyscore) {
        print("GOT SCORE: " + enyscore + " !");

        if (enemyScore != null)
            enemyScore.text = enyscore + "";
    }
}
