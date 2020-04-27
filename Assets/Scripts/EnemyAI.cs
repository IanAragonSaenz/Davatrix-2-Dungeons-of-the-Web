using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{


    public Transform target;

    public float speed ;
    public float nextWaypointDistance ;

    Path path;
    int currentWaypoint ;
    bool reachedEndOfpath;

    GameObject davalos;

    Seeker seeker;
    Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        speed = 5f;
        nextWaypointDistance = 3f;
        currentWaypoint = 0;
        reachedEndOfpath = false;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        davalos = GameObject.Find("Player");
        
        seeker.StartPath(this.transform.position, davalos.transform.position, OnPathComplete);
        

    }

    void OnPathComplete(Path p){
        if(!p.error){
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void fixedUpdate()
    {

        if ( path == null)
            return;
        /*float distance = Vector2.Distance(target.transform.position, transform.position);
        if (distance <= 40){
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }*/
        if( currentWaypoint >= path.vectorPath.Count){
            reachedEndOfpath = true;
            return;
        }else{
            reachedEndOfpath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;
        rb.AddForce(force);
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        
        if ( distance < nextWaypointDistance){
            currentWaypoint++;
        }
    }
}
