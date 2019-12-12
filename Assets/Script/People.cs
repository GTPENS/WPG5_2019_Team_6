using UnityEngine;

public class People : MonoBehaviour
{
    public int position;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = FindObjectOfType<Player>();
        int amount = player.amount;

        if (collision.tag == "Player")
        {
            FindObjectOfType<SpawnManager>().PickUp(position);

            SpriteRenderer p = player.GetComponent<SpriteRenderer>();

            if (amount == 0)
                p.color = new Color(238f / 255f, 53f / 255f, 224f / 255f);
            else
                p.color = new Color(138f / 255f, 73f / 255f, 204f / 255f);

            player.amount += 1;

            Destroy(this.gameObject);
        }
        
        
    }
}
