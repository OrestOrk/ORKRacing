using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Vector3 Offset;
    [SerializeField] private Transform Target;
    [SerializeField] private float TranslateSpeed, RotationSpeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HandleTranslation();
        HandleRotatinon();

    }
    private void HandleTranslation()
    {
        var TargetPosition = Target.TransformPoint(Offset);
        transform.position = Vector3.Lerp(transform.position, TargetPosition, TranslateSpeed * Time.fixedDeltaTime);

    }
    private void HandleRotatinon()
    {
        var Direction = Target.position - transform.position;
        var Rotation = Quaternion.LookRotation(Direction, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, Rotation, RotationSpeed * Time.fixedDeltaTime);
    }
}
