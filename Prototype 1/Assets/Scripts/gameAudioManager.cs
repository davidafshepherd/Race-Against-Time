//Libraries and Globals
using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

//Class responsible for handling the audio sources played within the Game and AIGame scenes
public class gameAudioManager : MonoBehaviour
{
    //This is where the variables that will be used in this class are declared
    private Rigidbody rigidBody;
    private float carEnginePitch;

    [SerializeField] private Transform playerRaceCar;
    [SerializeField] private Transform AIraceCar01;
    [SerializeField] private Transform AIraceCar02;
    [SerializeField] private Transform AIraceCar03;
    [SerializeField] private Transform AIraceCar04;

    [SerializeField] private AudioSource playerCarEngine;
    [SerializeField] private AudioSource AIcarEngine01;
    [SerializeField] private AudioSource AIcarEngine02;
    [SerializeField] private AudioSource AIcarEngine03;
    [SerializeField] private AudioSource AIcarEngine04;

    [SerializeField] private AudioSource raceMusic;
    [SerializeField] private AudioSource newLap;
    [SerializeField] private AudioSource buttonClicking;
    [SerializeField] private AudioSource menuAccess;

    //This method is called on initialisation and it turns every sound effect off at the start of the race
    //It also calls the RaceMusic co-routine
    void Start()
    {
        menuAccess.volume = 0;
        newLap.volume = 0;
        raceMusic.volume = 0;
        StartCoroutine(RaceMusic());
    }

    //This method is called once per frame to update the pitch of the engine sound of each race car
    void FixedUpdate()
    {
        CarEngineSound(playerRaceCar, playerCarEngine);
        CarEngineSound(AIraceCar01, AIcarEngine01);
        CarEngineSound(AIraceCar02, AIcarEngine02);
        CarEngineSound(AIraceCar04, AIcarEngine03);
        CarEngineSound(AIraceCar04, AIcarEngine04);
    }

    //This is the code that adjusts the pitch of the engine sound of a car according to it's speed
    void CarEngineSound(Transform raceCar, AudioSource carEngine)
    {
        rigidBody = raceCar.GetComponent<Rigidbody>();
        carEnginePitch = rigidBody.velocity.magnitude / 10;
        carEngine.pitch = carEnginePitch;
    }

    //This is the code that plays the race background music once the countdown finishes
    IEnumerator RaceMusic()
    {
        yield return new WaitForSeconds(3f);
        raceMusic.Play();
        raceMusic.volume = 0.25f;
    }

    //This is the code that plays a sound effect each time the player crosses the start line
    public void NewLapSound()
    {
        newLap.volume = 1;
        newLap.Play();
    }

    //This is the code that plays a button clicking sound effect each time a button is clicked
    public void ButtonClicking()
    {
        buttonClicking.volume = 1;
        buttonClicking.Play();
    }

    //This is the code that plays the menu access sound effect when the pause menu is accessed
    //It also disables the engine sound of the race cars
    public void PauseAudio()
    {
        menuAccess.volume = 1;
        menuAccess.Play();
        playerCarEngine.Pause();
        AIcarEngine01.Pause();
        AIcarEngine02.Pause();
        AIcarEngine03.Pause();
        AIcarEngine04.Pause();
    }

    //This is the code that plays the meny access sound effect when the pause menu is hidden
    //It also enables the engine sound of the race cars
    public void UnpauseAudio()
    {
        menuAccess.Play();
        playerCarEngine.Play();
        AIcarEngine01.Play();
        AIcarEngine02.Play();
        AIcarEngine03.Play();
        AIcarEngine04.Play();
    }
}
