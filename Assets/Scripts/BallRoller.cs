using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRoller : MonoBehaviour
{
    public int level = 1;
    public float torqueAmount;
    private Vector3 cameraOffset;

    private Vector2 originalPressPoint = Vector2.zero;
    private Rigidbody ballRb;
    [SerializeField]
    private int multiplier = 100;

    private void Awake()
    {
        MyEventSystem.I.StartLevel(level);
    }

    private void Start()
    {
        Transform playerBall = GameObject.Find("PlayerBall").transform;
        cameraOffset = playerBall.position - Camera.main.transform.position;
        ballRb = playerBall.GetComponent<Rigidbody>();
    }

    //used fixedUpdate & Time for consistancy accross different frame-rates
    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            originalPressPoint = Input.mousePosition;           
        }
        else if (Input.GetMouseButton(0))
        {
            //using Input.GetAxis for more precise/smooth caluation for torque
            float horizontalInput = Input.GetAxis("Mouse X");
            float verticalInput = Input.GetAxis("Mouse Y");
            Vector2 diff = new Vector2(horizontalInput, verticalInput).normalized;

            var movement = (Vector3.forward * -diff.x + Vector3.right * diff.y) * torqueAmount * Time.deltaTime;
            ballRb.AddTorque(movement * multiplier, ForceMode.VelocityChange);
            //Vector2 diff = (originalPressPoint - new Vector2(Input.mousePosition.x, Input.mousePosition.y)).normalized;
            //ballRigidbody.AddTorque((Vector3.forward * diff.x + Vector3.right * -diff.y) * torqueAmount, ForceMode.VelocityChange);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            ballRb.angularVelocity = Vector3.zero;
        }

    }
    private void LateUpdate()
    {
        Camera.main.transform.position = ballRb.transform.position - cameraOffset;
    }
}
