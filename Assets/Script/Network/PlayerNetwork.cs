using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerNetwork : MonoBehaviour
{
    bool masterAlive = true;
    bool clientAlive = true;
    public static PlayerNetwork Instance;
    public string PlayerName { get; private set; }

    public PhotonView PhotonView;
    private int PlayersInGame = 0;

    private void Awake()
    {
        Instance = this;
        PhotonView = GetComponent<PhotonView>();
        PlayerName = "Player #" + UnityEngine.Random.Range(1000, 9999);

        PhotonNetwork.sendRate = 60;
        PhotonNetwork.sendRateOnSerialize = 30;

        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode) {
        if(scene.name == "Game") {
            if (PhotonNetwork.isMasterClient)
                MasterLoadedGame();
            else
                NonMasterLoadedGame();
        }
    }
    void MasterLoadedGame() {
        PhotonView.RPC("RPC_LoadedGameScene", PhotonTargets.MasterClient);
        PhotonView.RPC("RPC_LoadGameOthers", PhotonTargets.Others);
    }

    void NonMasterLoadedGame() {
        PhotonView.RPC("RPC_LoadedGameScene", PhotonTargets.MasterClient);
    }

    [PunRPC]
    void RPC_LoadGameOthers() {
        PhotonNetwork.LoadLevel(3);
    }

    [PunRPC]
    void RPC_LoadedGameScene() {
        PlayersInGame++;
        if(PlayersInGame == 2) {
            print("All player ready");
            PhotonView.RPC("RPC_CreatePlayer", PhotonTargets.All);
        }
    }

    [PunRPC]
    private void RPC_CreatePlayer() {
        PhotonNetwork.Instantiate(Path.Combine("Prefab", "Player"), new Vector3(-10.5f, PhotonNetwork.isMasterClient ? 2f : -2f, 0), Quaternion.identity, 0);
    }


    // Gameplay
    public void Dead() {
        PhotonView.RPC("DeadMessage", PhotonTargets.All, PhotonNetwork.isMasterClient ? "Master" : "Client");
    }

    [PunRPC]
    void DeadMessage(string dead) {
        if (dead == "Master") {
            masterAlive = false;

            if (PhotonNetwork.isMasterClient)
                NetworkManager.instance.currentPlayer.GetComponent<Darah>().Dead(NetworkManager.instance.currentPlayer);
            else
                NetworkManager.instance.enemyPlayer.GetComponent<Darah>().Dead(NetworkManager.instance.enemyPlayer);
        }
        else if (dead == "Client") {
            clientAlive = false;

            if (!PhotonNetwork.isMasterClient)
                NetworkManager.instance.currentPlayer.GetComponent<Darah>().Dead(NetworkManager.instance.currentPlayer);
            else
                NetworkManager.instance.enemyPlayer.GetComponent<Darah>().Dead(NetworkManager.instance.enemyPlayer);
        }

        if (PhotonNetwork.isMasterClient && !masterAlive && !clientAlive) {
            TimeOff();
        }
    }

    public void TimeOff() {
        if (PhotonNetwork.isMasterClient) {
            int masterScore = Int32.Parse(NetworkManager.instance.currentPlayer.textScore.text);
            int enemyScore = Int32.Parse(NetworkManager.instance.currentPlayer.enemyScore.text);

            string winner = masterScore > enemyScore ? "Master" : masterScore < enemyScore ? "Client" : "Draw";

            PhotonView.RPC("WinnerMessage", PhotonTargets.All, winner);
        }
    }

    [PunRPC]
    void WinnerMessage(string winner) {
        if ((winner == "Master" && PhotonNetwork.isMasterClient) || (winner == "Client" && !PhotonNetwork.isMasterClient)) {
            GameManager.Instance.SetPanelText("You Win :)");
        }
        else if ((winner == "Master" && !PhotonNetwork.isMasterClient) || (winner == "Client" && PhotonNetwork.isMasterClient)) {
            GameManager.Instance.SetPanelText("You Lose :(");
        }
        else {
            GameManager.Instance.SetPanelText("Draw :|");
        }

        NetworkManager.instance.currentPlayer.End();
        NetworkManager.instance.enemyPlayer.End();

        GameManager.Instance.ShowPanel();
    }
}
