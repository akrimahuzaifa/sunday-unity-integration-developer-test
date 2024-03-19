using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRoller : MonoBehaviour
{
    [Header("x100")]
    public float torqueAmount;

    private Vector3 cameraOffset;
    private Rigidbody ballRb;
    private int _multiplier => 100;

    private void Awake()
    {
        int currentLvl = LevelManager.instance.GetActiveSceneIndex();
        MyEventSystem.I.StartLevel(currentLvl);
    }

    private void Start()
    {
        Transform playerBall = GameObject.Find("PlayerBall").transform;
        cameraOffset = playerBall.position - Camera.main.transform.position;
        ballRb = playerBall.GetComponent<Rigidbody>();
    }

    //used fixedUpdate & Time.deltatime for consistancy accross different frame-rates
    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            //using Input.GetAxis for more precise/smooth caluation for torque
            float horizontalInput = Input.GetAxis("Mouse X");
            float verticalInput = Input.GetAxis("Mouse Y");
            Vector2 diff = new Vector2(horizontalInput, verticalInput).normalized;

            var movement = (Vector3.forward * -diff.x + Vector3.right * diff.y) * torqueAmount * Time.deltaTime;
            ballRb.AddTorque(movement * _multiplier, ForceMode.VelocityChange);            
        }
        else if (Input.GetMouseButtonUp(0))
        {
            ballRb.angularVelocity = Vector3.zero;
        }
    }

    //Moved Camera position calculation to here for Smooth Camera Follow(execution order after update and fixed update)
    private void LateUpdate()
    {
        Camera.main.transform.position = ballRb.transform.position - cameraOffset;
    }
}
