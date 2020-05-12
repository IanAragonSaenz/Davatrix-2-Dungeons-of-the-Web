using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheBeastProjectile : MonoBehaviour
{

    public GameObject daddy;
    public GameObject baby;
  
    public Vector2 direction;

    public char attackType;

    public float attackARange;
    public float attackASpeed;
    public float attackASpeedIncrement;
    public float attackARangeIncrement;
    public bool attackBExplosion;
    List<Vector2> attackBPreviousPosition;
    int attackBListSize;

    float time;


    // Start is called before the first frame update
    void Start()
    {
        attackBPreviousPosition = new List<Vector2>();
        attackBListSize = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if( attackType == 'A'){
            time += Time.deltaTime * attackASpeed;
            direction = new Vector2(Mathf.Cos(time) * attackARange, Mathf.Sin(time) * attackARange);
            transform.position =  direction;
            attackARange += attackARangeIncrement;
            attackASpeed += attackASpeedIncrement;
        }

        if( attackType == 'B'){

            transform.position += (Vector3)direction * Time.deltaTime;
            attackBPreviousPosition.Add(transform.position);
            attackBListSize ++;

        }
    }

    void GenerateProjectile(int count){
        if( count > 0 ){
            float angle = Vector2.Angle(
                                        transform.position , 
                                        daddy.transform.position 
                                        ) * (Time.deltaTime * 3) ;

            Vector2 tempDirection = new Vector2( 
                                        -angle * Random.Range(0.1f,1.5f) , 
                                        -angle  * Random.Range(0.1f,1.5f)
                                    );

            GameObject projectile = Instantiate(
                                                baby, 
                                                attackBPreviousPosition[ 
                                                        ( attackBListSize >  1) ? 
                                                        attackBListSize - 3 : 0   ],
                                                Quaternion.identity
                                                );

            projectile.GetComponent<TheBeastProjectile>().direction = tempDirection;

            projectile.GetComponent<TheBeastProjectile>().attackType = 'B';

            projectile.GetComponent<TheBeastProjectile>().attackBExplosion = false;

            GenerateProjectile(count - 1);
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if( other.tag != "Boss Projectile"){
            if( attackType == 'B'){
                if( attackBExplosion) {

                    GenerateProjectile(Random.Range(4,6));

                    Destroy(this.gameObject);

                }else{
                    Destroy(this.gameObject);
                }
            }else{
                Destroy(this.gameObject);
            }
        }
    }
}
