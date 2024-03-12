//Libraries and Globals
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class responsible for handling the audio sources played within the TitleScreen scene
public class titleScreenAudioManager : MonoBehaviour
{
    //This is where the variables that will be used in this class are declared
    [SerializeField] private AudioSource buttonClicking;

    //This method is called on initialisation and it turns the button clicking sound effect off
    void Start()
    {
        buttonClicking.volume = 0;
    }

    //This is the code that plays a button clicking sound effect each time a button is clicked
    public void ButtonClicking()
    {
        buttonClicking.volume = 1;
        buttonClicking.Play();
    }
}
