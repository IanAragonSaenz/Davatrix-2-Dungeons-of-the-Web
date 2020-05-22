using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter(Collision other){

            Destroy(this.gameObject);
        
    }
}
