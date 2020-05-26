using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyramidHead : MonoBehaviour
{
    private struct Directions{
        public float x;
        public float y;
        public Directions(float x , float y){
            this.x = x;
            this.y = y;
        }
    };

    public GameObject baby;
    Directions[] directions;
    string[] orientations;
    int hp ;
    float cooldown;


    // Start is called before the first frame update
    void Start()
    {
        hp = 100;
        cooldown =  hp / (100 / 0.45f);
        directions = new Directions[10]{
            new Directions(0,0),
            new Directions(-11.8f,5),
            new Directions(-11.8f,-9.98f),
            new Directions(13.22f,-10.05f),
            new Directions(13.22f,4.95f),
            new Directions(-31.83f,-0.05f),
            new Directions(3.29f,-18.4f),
            new Directions(18.29f,14.88f),
            new Directions(0.56f,7.9f),
            new Directions(19.4f,-6.3f)
        };
        orientations = new string[16]{
            "N","NNE","NE","ENE","E","ESE","SE","SSE","S","SSW","SW",
            "WSW","W","WNW","NW","NNW"
        };
        Attack();
    }

    void Attack(){
        Directions spawn = directions[Random.Range(0,10)];
        List<string> seen = new List<string>();
        int amount = Random.Range(4,17);
        string[] points = new string[amount];
        points[0] = orientations[Random.Range(0,16)];
        seen.Add(points[0]);
        for(int i = 1 ; i < amount; i++){
            points[i] = SelectOrientation(seen);
        }
        
        for(int i = 0; i < amount; i++){
            Vector3 position = new Vector3(spawn.x,spawn.y,-1);
            GameObject projectile = Instantiate(baby,position,Quaternion.identity);
            projectile.GetComponent<PyramidHeadAttack>().orientation = points[i];
        }
        StartCoroutine(Wait());

    }

    public void Damaged(){
        hp -= 2;
        if( hp > 30){
            cooldown = hp / (100 / 0.45f);
        }
    }

    IEnumerator Wait(){
        yield return new WaitForSeconds(cooldown);
        Attack();
    }

    string SelectOrientation(List<string> previous){
        string temp = orientations[Random.Range(0,16)];
        while(previous.IndexOf(temp) != -1){
            temp = orientations[Random.Range(0,16)];
        }
        return temp;
    }
}
