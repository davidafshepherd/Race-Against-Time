//Libraries and Globals
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Class responsible for the countdown and handling the race car movement inputs at the start of the race
//It's also responsible for handling the pause menu accessibility at the start of the race 
public class AIcountdownController : MonoBehaviour
{
    //This is where the variables that will be used in this class are declared
    [SerializeField] private int countdownTime;
    [SerializeField] private Text countdownDisplay;
    [SerializeField] private Transform raceCar;
    private carController carController;

    [SerializeField] private Transform AIraceCar01;
    private AIcarController AIcarController01;
    [SerializeField] private Transform AIraceCar02;
    private AIcarController AIcarController02;
    [SerializeField] private Transform AIraceCar03;
    private AIcarController AIcarController03;
    [SerializeField] private Transform AIraceCar04;
    private AIcarController AIcarController04;

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
        AIcarController01 = AIraceCar01.GetComponent<AIcarController>();
        AIcarController01.EnableInput();
        AIcarController02 = AIraceCar02.GetComponent<AIcarController>();
        AIcarController02.EnableInput();
        AIcarController03 = AIraceCar03.GetComponent<AIcarController>();
        AIcarController03.EnableInput();
        AIcarController04 = AIraceCar04.GetComponent<AIcarController>();
        AIcarController04.EnableInput();

        pauseMenuManager = overlayCanvas.GetComponent<pauseMenuManager>();
        pauseMenuManager.EnableMenu();

        yield return new WaitForSeconds(1f);
        countdownDisplay.gameObject.SetActive(false);
    }
}
