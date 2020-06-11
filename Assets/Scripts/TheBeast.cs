using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheBeast : MonoBehaviour
{

    public GameObject baby;

    List<GameObject> attackAProjectiles;
    int size;
    int hp;
    float cooldown;
    bool damaged = false;




    // Start is called before the first frame update
    void Start()
    {
  
        size = -1;
        hp = 100;
        cooldown =(float) hp / (100/15);
        attackAProjectiles = new List<GameObject>();
        StartCoroutine( AttackA(0) );
      
    }
    

    IEnumerator AttackA(int attackAControl){
        
        if( attackAControl < 100) {
            GameObject projectile = Instantiate(baby, transform.position,Quaternion.identity);
            float range = Random.Range(0.001f,0.4f) ;
            float speed = Random.Range(0.8f,1f) * range;
            range *= (range % 2  ==  0  ) ? -1 : 1; 
            projectile.GetComponent<TheBeastProjectile>().attackType = 'A';
            projectile.GetComponent<TheBeastProjectile>().attackARangeIncrement = range / 60;
            projectile.GetComponent<TheBeastProjectile>().attackASpeedIncrement = range * Random.Range(0.08f,0.1f);
            projectile.GetComponent<TheBeastProjectile>().attackARange = range;
            projectile.GetComponent<TheBeastProjectile>().attackASpeed = speed;

            yield return new WaitForSeconds(0.05f);
            attackAControl ++;
            attackAProjectiles.Add(projectile);
            size++;
            StartCoroutine(AttackA(attackAControl));

        }else{
            AttackB(0, attackAProjectiles, size);
        }
    }


    void AttackB(int num, List<GameObject> attackAProjectiles, int size){

 
        if( num <= 360 ){
            
            Vector2 tempDirection =  new Vector2(num * Random.Range(-2f,2),num * Random.Range(-3f,2f)) * Time.deltaTime;
            GameObject projectile = Instantiate(
                                    baby, 
                                    transform.position,
                                    Quaternion.identity
                                    );
        
            projectile.GetComponent<TheBeastProjectile>().attackType = 'B';
            projectile.GetComponent<TheBeastProjectile>().attackBExplosion = true;
            projectile.GetComponent<TheBeastProjectile>().direction = tempDirection;
            if (size > 0){
                for(int i = 0; i <= size; i++){
                    Destroy(attackAProjectiles[i].gameObject);
                }
                attackAProjectiles.Clear();
                size = -1;
            }
            AttackB( num + 5, attackAProjectiles, size);

        }else{
            StartCoroutine(AttackPause(cooldown));
        }
        
    }


    IEnumerator AttackPause(float amount){
        yield return new WaitForSeconds(amount);

        Start();
        
    }


    public void Damaged(){
        hp -= 5;
        if( hp <= 100){
            Destroy(this.gameObject);
        }
        
    }
}
