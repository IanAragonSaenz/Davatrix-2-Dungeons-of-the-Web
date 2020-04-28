using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    //1->> needs a botom door
    //2->> needs a top door
    //3->> needs a left door
    //4->> needs a right door

    private RoomTemplates templates;
    private int rand;
    private bool spawned=false;
    //private float waitTime = 4f;
    void Start()
    {
        //Destroy(gameObject,waitTime);
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.1f);
    }

    void Spawn()
    {
        if (!spawned)
        {
            switch (openingDirection)
            {
                case 1:
                    

                        // spawns a bottom door room
                        rand = Random.Range(0, templates.bottomRooms.Length);
                        Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);
                        break;
                    
                case 2:
                    
                        // spawns a top door room
                        rand = Random.Range(0, templates.topRooms.Length);
                        Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);
                        break;
                    
                case 3:
                    
                        // spawns a left door room
                        rand = Random.Range(0, templates.leftRooms.Length);
                        Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
                        break;
                    
                case 4:
                    
                        // spawns a right door room
                        rand = Random.Range(0, templates.rightRooms.Length);
                        Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
                        break;
                    
                    
            }
            spawned = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        
            if (other.CompareTag("Spawnpoint")&&other.GetComponent<RoomSpawner>().spawned)
            {
                Instantiate(templates);
                Destroy(gameObject);
            }
        
    }
}
