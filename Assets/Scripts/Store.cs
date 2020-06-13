using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;


public class Store : MonoBehaviour
{
    public GameObject player;
    public Text playerMoney;

    void Start()
    {
        playerMoney.text = "$$: " + player.GetComponent<Player>().money;
    }

    public void BuyFill()
    {
        
        if(player.GetComponent<Player>().money >= 50)
        {

            player.GetComponent<Player>().money -= 50;
            player.GetComponent<Player>().currentHealth = player.GetComponent<Player>().maxHealth;
            player.GetComponent<Player>().TakeDamage(0);
        }
        playerMoney.text = "$$: " + player.GetComponent<Player>().money;
    }

    public void BuyRaise()
    {
        if (player.GetComponent<Player>().money >= 150)
        {
            player.GetComponent<Player>().money -= 150;
            player.GetComponent<Player>().maxHealth += 50;
            player.GetComponent<Player>().currentHealth = player.GetComponent<Player>().maxHealth;
            player.GetComponent<Player>().TakeDamage(0);
        }
        playerMoney.text = "$$: " + player.GetComponent<Player>().money;
    }
}
