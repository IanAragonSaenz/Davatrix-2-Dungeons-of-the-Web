using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour
{

    public GameObject bulletParticles;

    public float damage = 20f;
    

   void OnCollisionEnter2D(Collision2D collision)
    {
        if( collision.gameObject.tag == "player wall"){
            return;
        }
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
            Instantiate(bulletParticles, transform.position, Quaternion.identity);
            
            // daño a anonymous
            if( collision.gameObject == GameObject.Find("anonymous")){
                collision.gameObject.GetComponent<anonymous>().Damaged();
            }

            // daño a the beast
            if (collision.gameObject == GameObject.Find("The Beast"))
            {
                collision.gameObject.GetComponent<TheBeast>().Damaged();
            }

            // daño a second boss
            if (collision.gameObject == GameObject.Find("Pyramid Head"))
            {
                collision.gameObject.GetComponent<PyramidHead>().Damaged();
            }

        }
        Destroy(gameObject);
    }
}
