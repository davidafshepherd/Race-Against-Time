//Libraries and Globals
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class responsible for the movement of the camera
public class cameraController : MonoBehaviour
{
    //This is where the variables that will be used in this class are declared
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 eulerRotation;
    [SerializeField] private float damper;

    //This method is called on initialisation and sets the camera's rotation values as the pre-determined default ones
    void Start()
    {
        transform.eulerAngles = eulerRotation;
    }

    //This method is called once per frame to update the position of the camera
    void Update()
    {
        if (target == null)
            return;
        transform.position = Vector3.Lerp(transform.position, target.position + offset, damper * Time.deltaTime);
    }
}
