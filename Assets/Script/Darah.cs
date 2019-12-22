using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Darah : MonoBehaviour
{
    public const int maxHealth = 100;
    public int currentHealth = maxHealth;
    public RectTransform healthbar;

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if(healthbar != null)
            healthbar.sizeDelta = new Vector2(currentHealth * 2, healthbar.sizeDelta.y);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            //Destroy(gameObject);
            //SceneManager.LoadScene("Lose");

            //Dead(NetworkManager.instance.currentPlayer);
            PlayerNetwork.Instance.Dead();

            //GameManager.Instance.SetPanelText("Waiting Result...");
            GameManager.Instance.ShowPanel();
        }
    }

    public void Dead(Player p) {
        p.gameObject.SetActive(false);

        p.isDead = true;
    }
}
