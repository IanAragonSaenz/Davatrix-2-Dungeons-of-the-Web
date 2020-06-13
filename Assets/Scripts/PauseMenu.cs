using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    void Start(){
        Resume();
        StopStore();
    }
    public static bool GameIsPaused = false;
    public static int Pauseor = 0;
    public GameObject pauseMenuUI; 
    public GameObject storeMenuUI;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if (Pauseor == 2) StopStore();
            if (GameIsPaused){
                 Resume();
            }else{
                Pause();
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (Pauseor == 1) Resume();
            if (GameIsPaused)
            {
                StopStore();
            }
            else
            {
                Store();
            }
        }
    }
    public void Resume(){
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Pauseor = 0;
    }

    public void Menu(){
        SceneManager.LoadScene("main_menu");
    }

    
    void Pause(){
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Pauseor = 1;
    }

    
    public void QuitGame(){
       Application.Quit();
    }

    public void StopStore()
    {
        storeMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Pauseor = 0;
    }

    void Store()
    {
        storeMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Pauseor = 2;
    }
}
