using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baseEnemy : MonoBehaviour
{
    struct Attack
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

    Attack a1 = new Attack( Random.Range(1,30)  , Random.Range(1,10) );
    Attack a2 = new Attack( Random.Range(1,30)  , Random.Range(1,10) );
    Attack a3 = new Attack( Random.Range(1,30)  , Random.Range(1,10) );
    Attack a4 = new Attack( Random.Range(1,30)  , Random.Range(1,10) );
    GameObject davalos = GameObject.Find("Player");
    GameObject bullet = GameObject.Find("Enemy Projectile");

    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = (  Mathf.Sqrt( Mathf.Pow( davalos.transform.position.x - this.transform.position.x , 2  )  +
                            Mathf.Pow( davalos.transform.position.y - this.transform.position.y , 2  )  ) ) ;
        
        if ( distance <= 40){

            Attack attack = calculateAttack();
            Debug.Log("I");
            performAttack(attack);
            
        }else if ( distance <= 100){

            float tempX = ( davalos.transform.position.x < transform.position.x ) ? -1f : 1f;
            float tempY = ( davalos.transform.position.y < transform.position.y ) ? 1f : -1f;

            Vector2 temp = new Vector2(tempX * Time.deltaTime + transform.position.x ,  tempY * Time.deltaTime + transform.position.y);

            this.transform.position = temp;

        }
    }

    Attack calculateAttack()
    {
        float biasX = (Random.Range(0.33f, 0.97f) * 30) /2;
        float biasY = (Random.Range(0.33f,0.97f) * 10) / 2;

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

        float projectileX = Mathf.Pow( davalos.transform.position.x - transform.position.x , 2);
        float projectileY = Mathf.Pow( davalos.transform.position.y - transform.position.y , 2);
        float angle = Mathf.Sqrt( projectileX + projectileY );

        Quaternion angleQuanterion = new Quaternion(transform.position.x,transform.position.y,angle,0);

        Instantiate(bullet,this.transform.position, angleQuanterion);

        Cooldown(attack);
    }

    IEnumerator Cooldown(Attack attack){
        yield return new WaitForSeconds( attack.x / 10 );
    }
}
