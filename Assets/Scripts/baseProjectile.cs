using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baseProjectile : MonoBehaviour
{

    
    GameObject davalos;

    public float bulletDestroy;
    public bool followPlayer;
    public float projectileSpeed;

    float davalosX;
    float davalosY;

    // Start is called before the first frame update
    void Start()
    {

        davalos = GameObject.Find("Player");
        this.transform.localScale= new Vector2(projectileSpeed/10 + 4f, projectileSpeed/10 + 4f);

        // if the projectile does not follows the player, this will set the direction of the projectile.
        davalosX = davalos.transform.position.x;
        davalosY = davalos.transform.position.y;
                
        //Destroys the bullet after this time
        Destroy(this.gameObject, bulletDestroy + projectileSpeed / 5);

    }

    // Update is called once per frame
    void Update()
    {
        float tempSpeed  = ( projectileSpeed/10 > 0.5) ? 0.38f + (projectileSpeed / 10f ) - 0.5f : 
                           ( projectileSpeed / 10 > 0.3) ? projectileSpeed / 5f : 
                           (projectileSpeed / 10 > 0.19f) ? projectileSpeed / 2.5f : projectileSpeed / 1.3f ;

        
        if ( followPlayer ){

            this.transform.position = Vector3.MoveTowards(this.transform.position,
                                                          davalos.transform.position,
                                                          tempSpeed* Time.deltaTime )  ;
        
        }else{

            this.transform.position = Vector2.MoveTowards(transform.position, new Vector2(davalosX*10,davalosY*10), tempSpeed * Time.deltaTime);

        }
    }

    void OnCollisionEnter(Collision collision){
        
       Destroy(this.gameObject);

    }
}
