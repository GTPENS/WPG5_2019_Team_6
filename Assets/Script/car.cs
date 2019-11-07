using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class car : MonoBehaviour
{

    public GameObject[] bangs;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate( 0.0f, 10.0f * Time.deltaTime, 0.0f);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        int randomBang = Random.Range(0, bangs.Length);
        if (collision.gameObject.tag.Equals("Player"))
        {
            GameObject hit = collision.gameObject;
            Darah healthbar = hit.GetComponent<Darah>();

            if (healthbar != null)
            {
                healthbar.TakeDamage(35);
            }
            Instantiate(bangs[randomBang], collision.gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
            //SceneManager.LoadScene("Lose");

        }

        if (collision.gameObject.tag.Equals("Finish"))
        {

            Destroy(gameObject);
        }

    }


    }
