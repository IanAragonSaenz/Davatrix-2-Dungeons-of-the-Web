using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baseEnemy : MonoBehaviour
{
    private struct Attack
    {
        public float x;
        public float y;

        public Attack(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
    }

    private Attack a1;
    private Attack a2; 
    private Attack a3;
    private Attack a4;
    GameObject davalos ;

    public GameObject bullet;

    private float cooldown;
    public float cooldownControl = 6f;

    public int HP = 20;

    public GameObject playerWeapon;


    // Start is called before the first frame update
    void Start()
    {
        float x = Random.Range(5,25);
        float y = Random.Range(5,25);

        a1 = new Attack( Random.Range(5,30)  , Random.Range(2,10) );
        a2 = new Attack( Random.Range(5,30) , Random.Range(2,10) );
        a3 = new Attack( Random.Range(5,30)  , Random.Range(2,10) );
        a4 = new Attack( Random.Range(5,30) , Random.Range(2,10)  );
        

        cooldown = Time.time;
        davalos = GameObject.Find("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0){
            Instantiate(this,this.transform.position,Quaternion.identity);
            Destroy(this.gameObject);
        }
        
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position,davalos.transform.position,30f);
     
        if ( hit.collider != null ){
            Debug.Log("Super raycast message");
            GameObject tempObject = hit.collider.gameObject;
            if ( tempObject.gameObject != null){

                float distance = Vector2.Distance(davalos.transform.position,hit.transform.position);

                if(  distance <= 30){

                    if ( cooldown <= Time.time ){
                        
                        Attack attack = calculateAttack();
                        performAttack(attack);
                        cooldown = Time.time + (attack.x / 6 );
                    
                    }
                }
            }
        }
    }


    float F(float x){
        return (10 - (0.33f * x));
    }



    Attack calculateAttack()
    {
        
        float biasX =5;
        float biasY = 10;

        float tempX1 = ((F(a1.x) < a1.y) ? 0.25f : 0f );
        float tempX2 = ((F(a2.x) < a2.y) ? 0.25f : 0f );
        float tempX3 = ((F(a3.x) < a3.y) ? 0.25f : 0f );
        float tempX4 = ((F(a4.x) < a4.y) ? 0.25f : 0f );
        float dividedSpeed = (a1.x + a2.x + a3.x + a4.x) / 8;

        float tempY1 = ((F(a1.x) >= a1.y) ? 0.25f : 0f );
        float tempY2 = ((F(a1.x) >= a1.y) ? 0.25f : 0f );
        float tempY3 = ((F(a1.x) >= a1.y) ? 0.25f : 0f );
        float tempY4 = ((F(a1.x) >= a1.y) ? 0.25f : 0f );
        float dividedAttack = (a1.y + a2.y + a3.y + a4.y) /8;

        float A5Attack = dividedAttack + ( dividedAttack * ( biasY * ( tempY1 + tempY2 + tempY3 + tempY4)));
        float A5Speed = dividedSpeed + (biasX * ( tempX1 + tempX2 + tempX3 + tempX4));

        Attack a5 = new Attack(A5Speed, A5Attack);
        a1 = a2;
        a2 = a3;
        a3 = a4;
        a4 = a5;

        return a5;
    }


    void performAttack(Attack attack){

        //Creates a temp bullet object
        GameObject tempBullet = bullet;
        float bulletTime = Time.time + 10 * Time.deltaTime;

        //defines the speed of the bullet based on the calculated bullet speed, the line above the time it will take to destroy it
        tempBullet.GetComponent<baseProjectile>().projectileSpeed = attack.y;
        tempBullet.GetComponent<baseProjectile>().bulletDestroy = bulletTime;
        tempBullet.GetComponent<baseProjectile>().followPlayer = false;

        
        //Creates the instance of the new bullet
        GameObject enemyBulletSpawn = Instantiate(tempBullet,this.transform.position,Quaternion.identity);

        Destroy(enemyBulletSpawn, Time.time + 30 * Time.deltaTime);
        
        
    }

}