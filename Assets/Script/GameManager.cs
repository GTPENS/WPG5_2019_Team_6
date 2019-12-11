using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text textScore;
    public Player currentPlayer;

    public static GameManager Instance;

    private void Awake() {
        Instance = this;
    }
}
