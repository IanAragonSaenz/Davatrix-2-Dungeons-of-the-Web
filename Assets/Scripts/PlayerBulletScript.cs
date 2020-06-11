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
            
            if( collision.gameObject == GameObject.Find("anonymous")){
                collision.gameObject.GetComponent<anonymous>().Damaged();
            }
            
        }
        Destroy(gameObject);
    }
}
