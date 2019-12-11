using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    public GameObject playerPrefab;

    void Start()
    {
        GameObject p = PhotonNetwork.Instantiate(Path.Combine("Prefab", "Player"), new Vector3(-10.5f, PhotonNetwork.isMasterClient ? -2f : 2f, 0), Quaternion.identity, 0);

        FindObjectOfType<SpawnManager>().player = p.GetComponent<Player>();
    }
}
