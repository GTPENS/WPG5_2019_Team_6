using UnityEngine;

public class People : MonoBehaviour
{
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
        SpriteRenderer p = FindObjectOfType<Player>().GetComponent<SpriteRenderer>();
        p.color = new Color(238f / 255f, 53f / 255f, 224f / 255f);
        Destroy(this.gameObject);
    }
}
