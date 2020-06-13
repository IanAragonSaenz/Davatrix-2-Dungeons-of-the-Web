using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anonymousProjectiles : MonoBehaviour
{

    public bool moves;
    public Vector3 direction;

    // Start is called before the first frame update

    void Update(){

        if( moves ){

            transform.position += direction * Time.deltaTime * 9;
        }


    }

    void OnTriggerEnter2D(Collider2D other){
        if( other.tag == "boss projectile")
            return;

        if( other.tag == "bullet wall"){

            Destroy(this.gameObject);

        }

        if ( other.gameObject.tag == "Player"){

            Destroy(this.gameObject);

        }

        if( other.tag == "player projectile"){
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }


}
