using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyramidHeadAttack : MonoBehaviour
{
    public string orientation;
    public Vector2 direction;

    bool canDamage;
    
    bool canMove;

    // Start is called before the first frame update
    void Start()
    {
        switch( orientation ){
            case "E":
                direction = new Vector2(100f,0);
            break;
            case "W":
                direction = new Vector2(-100f,0);
            break;
            case "N":
                direction = new Vector2(0,100f);
            break;
            case "S":
                direction = new Vector2(0,-100f);
            break;
            case "SE":
                direction = new Vector2(100,-100);
            break;
            case "SW":
                direction = new Vector2(-100,-100);
            break;
            case "NW":
                direction = new Vector2(-100,100);
            break;
            case "NE":
                direction = new Vector2(100,100);
            break;
            case "NNE":
                direction = new Vector2(50,100);
            break;
            case "ENE":
                direction = new Vector2(100,50);
            break;
            case "ESE":
                direction = new Vector2(100,-50);
            break;
            case "SSE":
                direction = new Vector2(50,-100);
            break;
            case "SSW":
                direction = new Vector2(-50,-100);
            break;
            case "WSW":
                direction = new Vector2(-100,-50);
            break;
            case "WNW":
                direction = new Vector2(-100,50);
            break;
            case "NNW":
                direction = new Vector2(-50,100);
            break;
        }
        canMove = false;
        canDamage = false;

        StartCoroutine(Wait());

    }

    // Update is called once per frame
    void Update()
    {   
        if( canMove ){
            transform.position += (Vector3) direction * (Time.deltaTime / Random.Range(8,10)) ;
        }
         
    }
    IEnumerator Wait(){
        yield return new WaitForSeconds(2);
        canMove = true;
        canDamage = true;
    }
    void OnTriggerEnter2D(Collider2D other){
        if( other.tag == "Boss") return;
        if( other.tag != "Boss Projectile"){
            Destroy(this.gameObject);
        }
    }
}
