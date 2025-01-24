//Libraries and Globals
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class responsible for the movement, steering, braking and wheel movement of the race car sprite
public class carController : MonoBehaviour
{
    //This is where the variables that will be used in this class are declared
    private float verticalInput;
    private float horizontalInput;
    private float currentSteerAngle;
    private float currentBrakeForce;
    private bool isBraking;
    private Rigidbody rigidBody;
    public bool movementInput;
 
    [SerializeField] private float motorForce;
    [SerializeField] private float maxSteerAngle;
    [SerializeField] private float brakeForce;

    [SerializeField] private WheelCollider wheelColliderLF;
    [SerializeField] private WheelCollider wheelColliderRF;
    [SerializeField] private WheelCollider wheelColliderLB;
    [SerializeField] private WheelCollider wheelColliderRB;

    [SerializeField] private Transform centerOfMass;
    [SerializeField] private Transform wheelTransformLF;
    [SerializeField] private Transform wheelTransformRF;
    [SerializeField] private Transform wheelTransformLB;
    [SerializeField] private Transform wheelTransformRB;


    //This method is called on initialisation and gets the rigidbody component of race car sprite
    //It also disables the inputs for the movement of the race car sprite at the start of the race
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.centerOfMass = centerOfMass.localPosition;
        movementInput = false;
    }

    //This method is called once per frame to update all the important race car sprite details
    void FixedUpdate()
    {
        Movement();
        Steering();
        Braking();
        WheelMovement();
    }

    //This is the code that allows the race car sprite to move forwards/backwards
    void Movement()
    {
        if (movementInput)
        {
            verticalInput = Input.GetAxis("Vertical");
            wheelColliderLF.motorTorque = verticalInput * motorForce;
            wheelColliderRF.motorTorque = verticalInput * motorForce;
        }
    }

    //This is the code that allows the race car sprite to steer to the left/right
    void Steering()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        currentSteerAngle = maxSteerAngle * horizontalInput;
        wheelColliderLF.steerAngle = currentSteerAngle;
        wheelColliderRF.steerAngle = currentSteerAngle;
    }

    //This is the code that allows the race car sprite to slow down/brake
    void Braking()
    {
        isBraking = Input.GetKey(KeyCode.Space);
        currentBrakeForce = isBraking ? brakeForce : 0f;
        wheelColliderLF.brakeTorque = currentBrakeForce;
        wheelColliderRF.brakeTorque = currentBrakeForce;
        wheelColliderLB.brakeTorque = currentBrakeForce;
        wheelColliderRB.brakeTorque = currentBrakeForce;
    }

    //This calls the method that rotates wheels for each indivdual wheel
    void WheelMovement()
    {
        SingleWheelMovement(wheelColliderLF, wheelTransformLF);
        SingleWheelMovement(wheelColliderRF, wheelTransformRF);
        SingleWheelMovement(wheelColliderLB, wheelTransformLB);
        SingleWheelMovement(wheelColliderRB, wheelTransformRB);
    }


    //This is the code that rotates wheels according to the motion of the race car sprite
    public void SingleWheelMovement(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }

    //This is the class that enables the inputs for the movement of the race car sprite
    public void EnableInput()
    {
        movementInput = true;
    }
}
