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

    float F(float x){
        return (10 - (0.33f * x));
    }

 

    private Attack a1;
    private Attack a2; 
    private Attack a3;
    private Attack a4;
    GameObject davalos ;
    public GameObject bullet;
    private float cooldown;
    public float cooldownControl = 6f;


    // Start is called before the first frame update
    void Start()
    {
        float x = Random.Range(5,25);
        float y = Random.Range(5,25);

        a1 = new Attack( x*.25f  , y );
        a2 = new Attack( x * .5f , y *.75f );
        a3 = new Attack( x * .75f  , y *.5f );
        a4 = new Attack( x  , y *.25f  );

        cooldown = Time.time;
        davalos = GameObject.Find("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(davalos.transform.position, transform.position);
     
        if (distance < 25){
            if ( cooldown <= Time.time ){
                Attack attack = calculateAttack();
                performAttack(attack);
                cooldown = Time.time + (attack.x / 6 );
            }    
        }else if (distance < 50){

            transform.position = Vector2.MoveTowards(transform.position, davalos.transform.position, 0.2f);
        }
    }

    Attack calculateAttack()
    {
        float biasX =5; //(Random.Range(0.33f, 0.97f) * 30) /2;
        float biasY = 10; //(Random.Range(0.33f,0.97f) * 10) / 2;

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
        Instantiate(tempBullet,this.transform.position,Quaternion.identity);
        
        
    }
}