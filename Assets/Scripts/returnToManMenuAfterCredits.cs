using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class returnToManMenuAfterCredits : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (! GetComponent<AudioSource>().isPlaying ){

            SceneManager.LoadScene(3);

        }
    }
}
