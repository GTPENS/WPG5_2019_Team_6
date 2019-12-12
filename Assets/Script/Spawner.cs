using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Spawner : Photon.MonoBehaviour
{
    public GameObject car;
    public string nameObj;
    public float rotation;

    // Use this for initialization
    void Start()
    {
        if(PhotonNetwork.isMasterClient)
            StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            //GameObject.Instantiate(car, transform.position, car.transform.rotation);
            PhotonNetwork.InstantiateSceneObject(Path.Combine("Prefab", nameObj), transform.position, new Quaternion(0f, 0f, rotation, 0f), 0, null);
            yield return new WaitForSeconds(2.2f);
            
        }
    }


}
