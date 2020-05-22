using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour
{

    public GameObject bulletParticles;

    public float damage = 20f;
    

   void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            // falta poner que la bala haga daño al enemigo
            Instantiate(bulletParticles, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
