using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anonymous : MonoBehaviour
{
    public GameObject baby;
    float x ;
    float y;
    public int HP;

    public float cooldown;
    int attackCount;
    string[] directions =new string[] {"E", "W","N","S","SE","SW","NW","NE","NNE",
                             "ENE","ESE","SSE","SSW","WSW","WNW","NNW"};

    // Start is called before the first frame update
    void Start()
    {
        HP = 100;
        cooldown = 3;
        attackCount = 3;
        StartCoroutine(spawnAttack());
        StartCoroutine(spawnAttack());
        StartCoroutine(spawnAttack());
        StartCoroutine( movingAttack() ); 
        StartCoroutine( movingAttack() ); 
        StartCoroutine( movingAttack() ); 
  
     
    }

    void selectDirection(string orientation, ref float x, ref float y){
        switch( orientation ){
            case "E":
                x = 1;
                y = 0;

            break;
            case "W":
                x = -1;
                y = 0;
            break;
            case "N":
                x = 0;
                y = 1;
            break;
            case "S":
                x = 0;
                y = -1;
            break;
            case "SE":
                x = 1;
                y = -1;
            break;
            case "SW":
                x = -1;
                y = -1;
            break;
            case "NW":
                x = -1;
                y = 1;
            break;
            case "NE":
                x = 1;
                y = 1;
            break;
            case "NNE":
                x = 0.5f;
                y = 1;
            break;
            case "ENE":
                x = 1;
                y = 0.5f;
            break;
            case "ESE":
                x = 1;
                y = -0.5f;
            break;
            case "SSE":
                x = 0.5f;
                y = -1;
            break;
            case "SSW":
                x = -0.5f;
                y = -1;
            break;
            case "WSW":
                x = -1;
                y = -0.5f;
            break;
            case "WNW":
                x = -1;
                y = 0.5f;
            break;
            case "NNW":
                x = -0.5f;
                y = 1;
            break;
        }
    }

    void Update(){

        if( HP == 0){
            GameObject[] temp = GameObject.FindGameObjectsWithTag("Boss Projectile");
            foreach(GameObject x in temp){
                Destroy(x.gameObject);
            }
            Destroy(this.gameObject);
            HP = -1;
        }
    }

    IEnumerator movingAttack(){
        
        float x = 0 , y = 0;
        selectDirection(directions[Random.Range( 0, directions.Length) ] , ref x , ref y);

        GameObject[] projectiles = new GameObject[18];

        float increment = 1.25f;
        for(int i = 0; i <18;i ++){
            Vector3 temp = new Vector3(x,y,-1);
            projectiles[i] = Instantiate(baby,temp,Quaternion.identity);
            projectiles[i].GetComponent<anonymousProjectiles>().moves = false;
            Destroy(projectiles[i].gameObject, cooldown);
            yield return new WaitForSeconds( 0.1f );


            if( x > 0 )
                x += (x == 0.5f) ? 1 : increment;
            

            else if ( x < 0 )
                x -= (x == 0.5f) ? 1 : increment;

            if( y > 0 ) 
                y += (y == 0.5f) ? 1: increment;
                
            else if ( y < 0)
                y -= (y == 0.5f) ? 1: increment;

            if( i == 17 ){

                yield return new WaitForSeconds(1);
                float tempX = x , tempY = y;
                for(int j = 0; j < directions.Length ; j++){

                    selectDirection(directions[j], ref x, ref y);
                    Vector3 tempVector = new Vector3(x,y,0);
                    GameObject tempProjectile = Instantiate( baby , new Vector3(tempX,tempY,-1), Quaternion.identity);
                    tempProjectile.GetComponent<anonymousProjectiles>().moves = true;
                    tempProjectile.GetComponent<anonymousProjectiles>().direction = tempVector;
             

                }
            }

        }

        yield return new WaitForSeconds(0.5f);
        StopCoroutine(spawnAttack());
        StopCoroutine(movingAttack());
        StartCoroutine(movingAttack());

    }

    IEnumerator spawnAttack(){
        for(int i = 0;i <40; i++){
            selectDirection(directions[Random.Range(0,directions.Length)],ref x, ref y);
            Vector3 direction = new Vector3(x,y,0);
            GameObject projectile = Instantiate(baby,transform.position, Quaternion.identity);
            projectile.GetComponent<anonymousProjectiles>().direction = direction;
            projectile.GetComponent<anonymousProjectiles>().moves = true;
            yield return new WaitForSeconds( Random.Range( 0.1f, 0.2f));
        }
        yield return new WaitForSeconds(0.5f);
        StopCoroutine(spawnAttack());
        StopCoroutine(movingAttack());
        StartCoroutine(spawnAttack());
    }

    public void Damaged(){
        HP --;
    }
}
