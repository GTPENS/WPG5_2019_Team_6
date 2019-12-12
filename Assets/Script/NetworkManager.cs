using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManager : Photon.MonoBehaviour
{
    public Player currentPlayer;
    public Player enemyPlayer;
    public RectTransform healthbar;
    public Text textScore;
    public Text enemyScore;

    public static NetworkManager instance;

    void Awake()
    {
        instance = this;

        //moved
        //PhotonNetwork.Instantiate(Path.Combine("Prefab", "Player"), new Vector3(-10.5f, PhotonNetwork.isMasterClient ? 2f : -2f, 0), Quaternion.identity, 0);

        //PhotonNetwork.ConnectUsingSettings("1.0");
    }

    /*private void OnConnectedToMaster() {
        print("Trying connect master...");
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions() { MaxPlayers = 2 }, TypedLobby.Default);
    }

    void OnJoinedRoom() {
        print("JOINED ROOM");
        GameObject p = PhotonNetwork.Instantiate(Path.Combine("Prefab", "Player"), new Vector3(-10.5f, PhotonNetwork.isMasterClient ? 2f : -2f, 0), Quaternion.identity, 0);
    }*/

    public void InitMine(Player p) {
        currentPlayer = p;
        currentPlayer.GetComponent<Darah>().healthbar = healthbar;

        FindObjectOfType<CameraController>().target = currentPlayer.transform;
    }

    public void InitAll(Player p) {
        p.textScore = textScore;
        p.enemyScore = enemyScore;
        enemyPlayer = p;
    }
}
