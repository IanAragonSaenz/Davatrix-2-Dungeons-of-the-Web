using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forestGeneratorA : MonoBehaviour
{

    struct Location{
        public float x;
        public float y;
        public Location(float x , float y){
            this.x = x;
            this.y = y;
        }
    }

    static Location[] locations = {
        new Location( -42 , 84 ),
        new Location(-16,36),
        new Location(33,68),
        new Location(1,52),
        new Location(6,16),
        new Location(-48,-21.5f),
        new Location(34,-72),
        new Location(42,-9)
    };
    public GameObject enemy;

    float cooldown;
    
    // Start is called before the first frame update
    void Start()
    {
        cooldown = Time.time + 5f;
    }

    // Update is called once per frame
    void Update()
    {
        if( cooldown < Time.time){

            int index = Random.Range(0,7);
            GameObject meme = enemy;
            meme.transform.localScale = new Vector2(5,5);
            Instantiate(meme, new Vector3(locations[index].x, locations[index].y,-1), Quaternion.identity);
            cooldown = (Time.time + 200f) * Time.deltaTime;

        }
    }
}
