using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{

    public GameObject[] bottomRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;
    public GameObject[] topRooms;
    public GameObject closedRoom;

    public List<GameObject> rooms;



    //Aqui se puede modificar para hacer el cambio de nivel o la aparición del boss
    /*
     public float waitTime;
     private bool spawnedLastRoomThing;
     public GameObject thing;
     void Update(){
        if(waitTime<=0&&!spawnedLastRoomThing){
            if(!spawnedLastRoomThing){
                for(int i=0;i<rooms.Count-1;i++){
                    Instantiate(thing, rooms[i].transform.position, Quaternion.identity);
                    spawnedLastRoomThing=true;
                }
            }
        }else{
            
                waitTime-=Time.deltaTime;
            } 
     
    
    }
     
     
     */
}
