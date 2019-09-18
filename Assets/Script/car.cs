using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class car : MonoBehaviour
{
    

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

        if (collision.gameObject.tag.Equals("Player"))
        {

            SceneManager.LoadScene("Lose");

        }

        if (collision.gameObject.tag.Equals("Finish"))
        {

            Destroy(gameObject);
        }

    }


    }
