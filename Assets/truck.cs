using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class truck : MonoBehaviour
{
    float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(5.0f, 10.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0.0f, speed * Time.deltaTime, 0.0f);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag.Equals("Player"))
        {
            GameObject hit = collision.gameObject;
            Darah healthbar = hit.GetComponent<Darah>();

            if (healthbar != null)
            {
                healthbar.TakeDamage(35);
            }
            Destroy(gameObject);
            //SceneManager.LoadScene("Lose");

        }

        if (collision.gameObject.tag.Equals("Finish"))
        {

            Destroy(gameObject);
        }

    }
}
