using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject panelStatus;
    public GameObject buttonBackPanel;
    public Text textPanel;

    public static GameManager Instance;

    private void Awake() {
        Instance = this;
    }

    public void ShowPanel() {
        panelStatus.SetActive(true);
    }

    public void SetPanelText(string teks) {
        textPanel.text = teks;
    }
    public void ShowButtonBack() {
        buttonBackPanel.SetActive(true);
    }
}
