using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject car;

    // Use this for initialization
    void Start()
    {

   
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {

        
    }


    IEnumerator Spawn()
    {
        while (true)
        {
            GameObject.Instantiate(car, transform.position, car.transform.rotation);
            yield return new WaitForSeconds(2.2f);
            
        }
    }


}
