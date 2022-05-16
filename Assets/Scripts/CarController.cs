using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private float horizontalInput, vertivalInput;
    private float currentSteerAngle;
    private float currentBreakingForce;
    [SerializeField] float motorForce, breakForce;
    [SerializeField] float MaxSteerAngle;

    [SerializeField] WheelCollider ForwardLeftWheelCollider;
    [SerializeField] WheelCollider ForwardRigthWheelCollider;
    [SerializeField] WheelCollider BackLeftWheelCollider;
    [SerializeField] WheelCollider BackRigthWheelCollider;

    [SerializeField] Transform ForwardLeftWheelTransform;
    [SerializeField] Transform ForwardRigthWheelTransform;
    [SerializeField] Transform BackLeftWheelTransform;
    [SerializeField] Transform BackRigthWheelTransform;

    private bool isBreaking;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }
    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        vertivalInput = Input.GetAxis("Vertical");

        isBreaking = Input.GetKeyDown(KeyCode.Space);
    }
    private void HandleMotor()
    {
        ForwardLeftWheelCollider.motorTorque = vertivalInput * motorForce;
        ForwardRigthWheelCollider.motorTorque = vertivalInput * motorForce;

        currentBreakingForce = isBreaking ? currentBreakingForce : 0;

        if (isBreaking)
        {
            ApplyBreaking();
        }
    }
    private void ApplyBreaking()
    {
        ForwardLeftWheelCollider.motorTorque = currentBreakingForce;
        ForwardRigthWheelCollider.motorTorque = currentBreakingForce;
        BackLeftWheelCollider.motorTorque = currentBreakingForce;
        BackRigthWheelCollider.motorTorque = currentBreakingForce;
    }
    private void HandleSteering()
    {
        currentSteerAngle = MaxSteerAngle * horizontalInput;

        ForwardLeftWheelCollider.steerAngle = currentSteerAngle;
        ForwardRigthWheelCollider.steerAngle = currentSteerAngle;
    }
    private void UpdateWheels()
    {
        UpdateSingleWheel(ForwardLeftWheelCollider, ForwardLeftWheelTransform);
        UpdateSingleWheel(ForwardRigthWheelCollider, ForwardRigthWheelTransform);
        UpdateSingleWheel(BackLeftWheelCollider, BackLeftWheelTransform);
        UpdateSingleWheel(BackRigthWheelCollider, BackRigthWheelTransform);
    }
    private void UpdateSingleWheel(WheelCollider wheelCollider,Transform wheelTransform)
    {
        Vector3 Pos;
        Quaternion Rot;
        wheelCollider.GetWorldPose(out Pos, out Rot);
        wheelTransform.rotation = Rot;
        wheelTransform.position = Pos;
    }
}
