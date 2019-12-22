using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Timer : MonoBehaviour
{
    Image timeBar;
    public float maxTime = 60f;
    float timeLeft;
    bool status = false;


    void Start()
    {
        timeBar = GetComponent<Image>();
        timeLeft = maxTime;

    }

    void Update()
    {
        if (!PlayerNetwork.Instance.clientAlive && !PlayerNetwork.Instance.masterAlive) {
            status = true;
        }
        else if (timeLeft > 0 && !status) {
            //if (!Settings.status) return;
            timeLeft -= Time.deltaTime;
            timeBar.fillAmount = timeLeft / maxTime;
        }
        else if (!status) {
            status = true;
            PlayerNetwork.Instance.TimeOff();
            // SceneManager.LoadScene("Lose");
        }

    }
}
