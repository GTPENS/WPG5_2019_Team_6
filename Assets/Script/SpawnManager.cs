using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public PeopleSpawner[] spawnerPoints;

    void Start() {
        Spawn();
    }

    public void Spawn() {
        int rand = Random.Range(0, spawnerPoints.Length);
        spawnerPoints[rand].Spawn();
    }
}
