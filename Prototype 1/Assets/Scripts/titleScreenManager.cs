//Libraries and Globals
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Class responsible for the functionality of the buttons within the title screen, the options and controls menu
public class titleScreenManager : MonoBehaviour
{
    //This is the code that loads the Game scene when the "Start Race" button is pressed
    public void LoadRace()
    {
        SceneManager.LoadScene(1);
    }

    //This is the code that loads the AIGame scene when the "Start AI Race" button is pressed
    public void LoadAIRace()
    {
        SceneManager.LoadScene(2);
    }

    //This is the code that closes down the game when the "Quit" button is pressed
    public void ExitGame()
    {
        Debug.Log("Quit Successful");
        Application.Quit();
    }
}
