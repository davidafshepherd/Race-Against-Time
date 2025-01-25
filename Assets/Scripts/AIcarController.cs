//Libraries and Globals
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class responsible for the movement, steering and and wheel movement of the AI race cars
public class AIcarController : MonoBehaviour
{
    //This is where the variables that will be used in this class are declared
    private Rigidbody rigidBody;
    [SerializeField] private Transform centerOfMass;

    [SerializeField] private Transform path;
    private Transform[] pathTransforms;
    private List<Transform> nodes;
    private int currentNode = 0;

    [SerializeField] WheelCollider wheelColliderLF;
    [SerializeField] WheelCollider wheelColliderRF;
    [SerializeField] WheelCollider wheelColliderLB;
    [SerializeField] WheelCollider wheelColliderRB;
    [SerializeField] Transform wheelTransformLF;
    [SerializeField] Transform wheelTransformRF;
    [SerializeField] Transform wheelTransformLB;
    [SerializeField] Transform wheelTransformRB;

    [SerializeField] private Transform raceCar;
    private carController carController;
    private bool movementInput;

    [SerializeField] private float maxSteerAngle;
    private Vector3 relativeVector;
    private float newSteer;
    [SerializeField] private float currentSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float maxMotorTorque;

    //This method is called on initialisation and gets the rigidbody component of AI race cars
    //It also creates a path for the AI race cars to follow using the path nodes created
    //It also disables the movement of the AI race cars at the start of the race
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.centerOfMass = centerOfMass.localPosition;
        movementInput = false;
        carController = raceCar.GetComponent<carController>();

        pathTransforms = path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();
        for(int i = 0; i < pathTransforms.Length; i++)
        {
            if(pathTransforms[i] != path)
            {
                nodes.Add(pathTransforms[i]);
            }
        }
    }

    //This method is called once per frame to update all the important AI race cars' details
    //It also updates the path that the AI race cars should follow
    void FixedUpdate()
    {
        Steering();
        Movement();
        NodeUpdater();
        carController.SingleWheelMovement(wheelColliderLF, wheelTransformLF);
        carController.SingleWheelMovement(wheelColliderRF, wheelTransformRF);
        carController.SingleWheelMovement(wheelColliderLB, wheelTransformLB);
        carController.SingleWheelMovement(wheelColliderRB, wheelTransformRB);
    }

    //This is the code that allows the AI race car to steer towards the next path node
    void Steering()
    {
        relativeVector = transform.InverseTransformPoint(nodes[currentNode].position);
        newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;
        wheelColliderLF.steerAngle = newSteer;
        wheelColliderRF.steerAngle = newSteer;
    }

    //This is the code that allows the AI race cars to move towards the next path node
    void Movement()
    {
        if (movementInput)
        {
            currentSpeed = 2 * Mathf.PI * wheelColliderLF.radius * wheelColliderLF.rpm * 60 / 1000;
            if (currentSpeed < maxSpeed)
            {
                wheelColliderLF.motorTorque = maxMotorTorque;
                wheelColliderRF.motorTorque = maxMotorTorque;
            }
            else
            {
                wheelColliderLF.motorTorque = 0;
                wheelColliderRF.motorTorque = 0;
            }
        }
    }

    //This is the code that updates the next node for the AI race cars to follow
    void NodeUpdater()
    {
        if (Vector3.Distance(transform.position, nodes[currentNode].position) < 3f)
        {
            if (currentNode == nodes.Count - 1)
            {
                currentNode = 0;
            }
            else
            {
                currentNode++;
            }
        }
    }

    //This is the code that enables the inputs for the movement of the AI race cars
    public void EnableInput()
    {
        movementInput = true;
    }
}
