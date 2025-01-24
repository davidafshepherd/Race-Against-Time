//Libraries and Globals
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Class responsible for the functionality of the pause menu
public class pauseMenuManager : MonoBehaviour
{
    //This is where the variables that will be used in this class are declared
    private bool allowPause;
    [SerializeField] private GameObject pauseMenu;
    private bool gameIsPaused;

    [SerializeField] private GameObject audioManager;
    private gameAudioManager audioManagerScript;

    //This method is called on initialisation and it stops the pause menu from being enabled
    //It also sets the pause menu as not being enabled
    void Start()
    {
        allowPause = false;
        gameIsPaused = false;

        audioManagerScript = audioManager.GetComponent<gameAudioManager>();
    }

    //This method is called once per frame to check if the ESC key is being pressed or not
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (allowPause)
            {
                if (gameIsPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }
    }

    //This is the code that disables the pause menu
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        audioManagerScript.UnpauseAudio();
    }

    //This is the code that enables the pause menu
    void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
        audioManagerScript.PauseAudio();
    }

    //This is the code that allows the pause menu to be enabled again
    public void EnableMenu()
    {
        allowPause = true;
    }

    //This is the code that restarts the current race
    public void RestartRace()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    //This is the code that stops the current race
    //It also returns the player to the title screen
    public void QuitRace()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
}
