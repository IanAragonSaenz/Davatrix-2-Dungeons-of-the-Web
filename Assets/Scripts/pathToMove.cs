using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class pathToMove : MonoBehaviour
{

    
    
    public GameObject target;
    public float speed = 2f;
    bool moving;
    public float nextWayPointDistance = 3f;

    Path path;
    int currentWayPoint =0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        //seeker.StartPath(transform.position,GameObject.Find("Player").transform.position, OnPathComplete);
        moving = false;
        
    }

    void OnPathComplete(Path p){

        if ( !p.error){
            path = p;

            currentWayPoint = 0;

        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {

       float distance = Vector2.Distance(target.transform.position,transform.position);
        if( distance <= 30 && distance >= 15 ){
            seeker.StartPath(transform.position,target.transform.position);
            
            Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - (Vector2)transform.position).normalized;
            Vector2 force = direction * speed * Time.deltaTime;

            
            distance = Vector2.Distance(transform.position, path.vectorPath[currentWayPoint]);

            if ( distance < nextWayPointDistance){
                currentWayPoint++;
            }
        }else{
            path = null;
        }

        if ( currentWayPoint >= path.vectorPath.Count){
            reachedEndOfPath = true;
            moving = false;
            return;
        }else{
            reachedEndOfPath = false;
        }


        
    }
}
