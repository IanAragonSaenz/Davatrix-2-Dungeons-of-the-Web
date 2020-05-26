using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anonymous : MonoBehaviour
{
    public GameObject baby;

    // Start is called before the first frame update
    void Start()
    {
     StartCoroutine( movingAttack() ); 
    }


    IEnumerator movingAttack(){
        float direction = 1;
        GameObject[] projectiles = new GameObject[144];
        int projectileIndex = 0, prevIndex = 0;
        for(int i = 0; i <18;i ++){
            //E
            Vector3 temp = new Vector3(transform.position.x + direction,transform.position.y,-1);
            projectiles[projectileIndex] = Instantiate(baby,temp,Quaternion.identity);
            projectileIndex++;
            yield return new WaitForSeconds(0.01f);


            //NE
            temp = new Vector3(transform.position.x + direction ,transform.position.y + direction,-1);
            projectiles[projectileIndex] = Instantiate(baby,temp,Quaternion.identity);
            projectileIndex++;
            yield return new WaitForSeconds(0.01f);

            //N
            temp = new Vector3(transform.position.x, transform.position.y + direction, -1);
            projectiles[projectileIndex] = Instantiate(baby,temp,Quaternion.identity);
            projectileIndex++;
            yield return new WaitForSeconds(0.01f);

            //NW
            temp = new Vector3(transform.position.x - direction, transform.position.y + direction, -1);
            projectiles[projectileIndex] = Instantiate(baby,temp,Quaternion.identity);
            projectileIndex++;
            yield return new WaitForSeconds(0.01f);

       

            //W
            temp = new Vector3(transform.position.x - direction, transform.position.y, -1);
            projectiles[projectileIndex] = Instantiate(baby,temp,Quaternion.identity);
            projectileIndex++;
            yield return new WaitForSeconds(0.01f);

            //SW
            temp = new Vector3(transform.position.x - direction, transform.position.y - direction, -1);
            projectiles[projectileIndex] = Instantiate(baby,temp,Quaternion.identity);
            projectileIndex++;
            yield return new WaitForSeconds(0.01f);

            //S
            temp = new Vector3(transform.position.x, transform.position.y - direction, -1);
            projectiles[projectileIndex] = Instantiate(baby,temp,Quaternion.identity);
            projectileIndex++; 
            yield return new WaitForSeconds(0.01f);

            //SE
            temp = new Vector3(transform.position.x + direction, transform.position.y - direction, -1);
            projectiles[projectileIndex] = Instantiate(baby,temp,Quaternion.identity);
            projectileIndex++;
            yield return new WaitForSeconds(0.01f);

            for(int j = prevIndex; j < projectileIndex; j++){
                projectiles[j].GetComponent<anonymousProjectiles>().attackType = 'A';
                projectiles[j].GetComponent<anonymousProjectiles>().AttackAradius = direction;
            }

            direction += 1.25f;
            prevIndex = projectileIndex;

        }

        for(int i = 0; i < projectileIndex; i++){
            projectiles[i].GetComponent<anonymousProjectiles>().AttackACanMove = true;
        }

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
