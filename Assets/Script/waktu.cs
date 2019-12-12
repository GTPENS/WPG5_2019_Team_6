using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class waktu : MonoBehaviour
{
    Image timeBar;
    public float maxTime = 60f;
    float timeLeft;


    void Start()
    {
        timeBar = GetComponent<Image>();
        timeLeft = maxTime;

    }

    void Update()
    {
        if (timeLeft > 0)
        {
            //if (!Settings.status) return;
            timeLeft -= Time.deltaTime;
            timeBar.fillAmount = timeLeft / maxTime;
        }
        else
        {
            PlayerNetwork.Instance.TimeOff();
            // SceneManager.LoadScene("Lose");
        }

    }
}
