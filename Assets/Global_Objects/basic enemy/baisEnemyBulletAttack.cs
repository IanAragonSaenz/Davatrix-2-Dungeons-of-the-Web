using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baisEnemyBulletAttack : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 direction;
    public float speed  = 2;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * Time.deltaTime * speed;
    }


    void OnTriggerEnter2D(Collider2D other){

        if( other.tag == "player"){

        }

        if( other.tag != "Normal projectile" && other.tag != "Enemy")
            Destroy(this.gameObject);
    }
}
