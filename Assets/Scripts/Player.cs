using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    public int maxHealth = 100;
    public int currentHealth;
    public int money;
    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        money = 10;
    }

    // Update is called once per frame
    void Update()
    {    
        if(Input.GetKeyDown(KeyCode.Space)){
            TakeDamage(20);
        }
    }

    public void TakeDamage(int damage){
        currentHealth-=damage;
        healthBar.SetHealth(currentHealth);
    }

    void OnTriggerEnter2D(Collider2D other){
    	if(other.gameObject.tag=="Boss Projectile"){
 			TakeDamage(10);
    	}
 	}

    
}
