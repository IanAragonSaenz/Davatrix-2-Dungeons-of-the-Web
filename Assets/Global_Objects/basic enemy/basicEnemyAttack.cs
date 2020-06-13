using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicEnemyAttack : MonoBehaviour
{
    public GameObject baby;
    public string orientation;
    public int HP;
    float x;
    float y;

    // Start is called before the first frame update
    void selectDirection(string orientation){
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
            default:
                x = 1;
                y = 1;
                break;
        }
    }
    void Start()
    {
        
        StartCoroutine( Attack() );

        HP = 30;
    }

    public void Damaged()
    {
        HP -= 3;

        if (HP <= 0)
        {
            GameObject.Find("Player").GetComponent<Player>().money += 8;
            Destroy(this.gameObject);
        }

    }

    IEnumerator Attack(){


        for(int i = 0; i < 10; i++){
                selectDirection(orientation);
                Vector3 direction = new Vector3(x,y,0);
                GameObject projectile = Instantiate(baby,transform.position,Quaternion.identity);
                projectile.GetComponent<baisEnemyBulletAttack>().direction = direction;
                projectile.GetComponent<baisEnemyBulletAttack>().speed = 3;
                yield return new WaitForSeconds(0.4f);

            }
        yield return new WaitForSeconds(2);
        StartCoroutine( Attack() );
    }

}
