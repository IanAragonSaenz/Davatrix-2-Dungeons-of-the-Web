using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baseProjectile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector3 temp = new Vector3(transform.position.x,transform.position.y,Mathf.Sqrt( Mathf.Pow(transform.position.z,2) - 
                                                                               Mathf.Pow(GameObject.Find("Player").transform.position.z , 2 )));
        transform.position = temp;
       

    }

    // Update is called once per frame
    void Update()
    {

       this.transform.position = Vector3.MoveTowards(transform.position,GameObject.Find("Player").transform.position,0.8f);
        
    }

    void OnCollisionEnter(Collision collision){
        
        Destroy(this);
    }
}
