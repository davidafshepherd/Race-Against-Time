//Libraries and Globals
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Class responsible for managing the race information displayed on the screen
public class informationManager : MonoBehaviour
{

    //This is where the variables that will be used in this class are declared
    public float bestLapTime = Mathf.Infinity;
    public float previousLapTime = 0;
    public float currentLapTime = 0;
    public int currentLap = 0;

    private float lapTimerTimestamp;
    private int lastCheckpointPassed = 0;

    private Transform checkpointsParent;
    private int checkpointCount;
    private int checkpointLayer;

    public Text currentLapDisplay;
    public Text currentLapTimeDisplay;
    public Text previousLapTimeDisplay;
    public Text bestLapTimeDisplay;

    [SerializeField] private GameObject audioManager;
    private gameAudioManager audioManagerScript;

    //This method is called on initialisation and it gets the checkpoint layer
    //It also gets the number of checkpoints and the checkpoint game objects themselves
    void Awake()
    {
        checkpointsParent = GameObject.Find("checkpoints").transform;
        checkpointCount = checkpointsParent.childCount;
        checkpointLayer = LayerMask.NameToLayer("checkpoint");

        audioManagerScript = audioManager.GetComponent<gameAudioManager>();
    }

    //This is the code responsible for the start of a lap
    //It increments the lap number and resets the last checkpoint passed
    //It also stores the time elapsed since the start of the race 
    void StartLap()
    {
        currentLap++;
        lastCheckpointPassed = 1;
        lapTimerTimestamp = Time.time;
    }

    //This is the code responsible for the end of a lap
    //It calculates the previous and best lap times
    void EndLap()
    {
        previousLapTime = Time.time - lapTimerTimestamp;
        bestLapTime = Mathf.Min(previousLapTime, bestLapTime);
        audioManagerScript.NewLapSound();
    }

    //This is the code that checks if the checkpoints are being triggered in the correct order
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer != checkpointLayer)
        {
            return;
        }
        if (collider.gameObject.name == "1")
        {
            if (lastCheckpointPassed == checkpointCount)
            {
                EndLap();
            }
            if (currentLap == 0 || lastCheckpointPassed == checkpointCount)
            {
                StartLap();
            }
            return;
        }
        if (collider.gameObject.name == (lastCheckpointPassed + 1).ToString())
        {
            lastCheckpointPassed++;
        }
    }

    //This method is called once per frame to update the current lap time
    //It also updates the race information displayed on the screen
    void Update()
    {
        currentLapTime = lapTimerTimestamp > 0 ? Time.time - lapTimerTimestamp : 0;
        currentLapDisplay.text = $"LAP: {currentLap}";
        currentLapTimeDisplay.text = $"TIME: {(int)currentLapTime / 60}:{(currentLapTime) % 60:00.000} ";
        previousLapTimeDisplay.text = previousLapTime != 0 ? $"LAST: {(int)previousLapTime / 60}:{(previousLapTime) % 60:00.000} " : "LAST: NONE";
        bestLapTimeDisplay.text = bestLapTime < 1000000 ? $"BEST: {(int)bestLapTime / 60}:{(bestLapTime) % 60:00.000} " : "BEST: NONE";
    }
}
