using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public PeopleSpawner[] spawnerPoints;
    public Player player;

    bool[] spawnerStatus = { false, false };

    void Start() {
        SpawnAll();
    }

    public void Spawn() {
        int rand = Random.Range(0, spawnerPoints.Length);

        if (!spawnerStatus[rand]) {
            spawnerPoints[rand].Spawn();
            spawnerStatus[rand] = true;
        }
    }

    public void SpawnAll() {
        if(NetworkManager.instance.currentPlayer != null)
            NetworkManager.instance.currentPlayer.speed = 10;

        if (!spawnerStatus[0])
            spawnerPoints[0].Spawn();

        if (!spawnerStatus[1])
            spawnerPoints[1].Spawn();

        spawnerStatus[0] = true;
        spawnerStatus[1] = true;
    }

    public void PickUp(int position, Player p) {
        spawnerStatus[position] = false;
        p.speed -= 3;
    }
}
