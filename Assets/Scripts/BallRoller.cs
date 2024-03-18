using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRoller : MonoBehaviour
{
    public int level = 1;
    public float torqueAmount;
    private Vector3 cameraOffset;

    private bool isPressing = false;
    private Vector2 originalPressPoint = Vector2.zero;

    private void Awake()
    {
        MyEventSystem.I.StartLevel(level);
    }

    private void Start()
    {
        cameraOffset = GameObject.Find("PlayerBall").transform.position - Camera.main.transform.position;
    }

    private void Update()
    {
        var ballRigidbody = GameObject.Find("PlayerBall").GetComponent<Rigidbody>();

        if (Input.GetMouseButton(0))
        {
            if (!isPressing)
            {
                originalPressPoint = Input.mousePosition;
                isPressing = true;
            }
            else
            {
                Vector2 diff = (originalPressPoint - new Vector2(Input.mousePosition.x, Input.mousePosition.y)).normalized;
                
                ballRigidbody.AddTorque((Vector3.forward * diff.x + Vector3.right * -diff.y) * torqueAmount, ForceMode.VelocityChange);
            }
        }
        else
        {
            isPressing = false;
        }

        Camera.main.transform.position = ballRigidbody.transform.position - cameraOffset;
    }
}
