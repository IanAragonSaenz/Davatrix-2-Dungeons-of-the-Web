using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void update(){
        if( Input.GetMouseButtonDown(0)){
            StartButton();
        }
    }
    

    public void StartButton(){

        SceneManager.LoadScene(0);

    }
}
