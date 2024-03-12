//Libraries and Globals
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Class responsible for the countdown and handling the race car movement inputs at the start of the race
//It's also responsible for handling the pause menu accessibility at the start of the race 
public class countdownController : MonoBehaviour
{
    //This is where the variables that will be used in this class are declared
    [SerializeField] private int countdownTime;
    [SerializeField] private Text countdownDisplay;
    [SerializeField] private Transform raceCar;
    private carController carController;

    [SerializeField] private GameObject overlayCanvas;
    private pauseMenuManager pauseMenuManager;

    //This method is called on initialisation and it calls the countdown co-routine
    void Start()
    {
        StartCoroutine(Countdown());
    }

    //This is the code that starts the countdown and decrements it at the start of the race
    //It also enables the inputs for the movement of the race cars once the countdown is finished
    //It also allows the pause menu to be enabled again once the countdown is finished
    IEnumerator Countdown()
    {
        while (countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }
        countdownDisplay.text = "GO!";

        carController = raceCar.GetComponent<carController>();
        carController.EnableInput();

        pauseMenuManager = overlayCanvas.GetComponent<pauseMenuManager>();
        pauseMenuManager.EnableMenu();

        yield return new WaitForSeconds(1f);
        countdownDisplay.gameObject.SetActive(false);
    }
}
