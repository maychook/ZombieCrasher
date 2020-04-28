﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    public Transform target;

    public float distance = 6.3f; // distance on the Z axis
    public float height = 3.5f;

    public float height_Damping = 3.25f;
    public float rotation_Damping = 0.27f;


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // LateUpdate is called after FixedUpdate finishes
    void LateUpdate()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        // calculate the current rotation angle
        float wanted_Rotation_Angle = target.eulerAngles.y;
        float wanted_Height = target.position.y + height;

        float current_Rotation_Angle = transform.eulerAngles.y;
        float current_Height = transform.position.y;

        // it will rotate the camera will lerping in the given time
        // will slowly go from current to wanted in the given time
        current_Rotation_Angle = Mathf.LerpAngle(
            current_Rotation_Angle, wanted_Rotation_Angle, rotation_Damping * Time.deltaTime);

        Quaternion current_Rotation = Quaternion.Euler(0f, current_Rotation_Angle, 0f);

        transform.position = target.position;
        transform.position -= current_Rotation * Vector3.forward * distance;

        transform.position = new Vector3(transform.position.x, current_Height, transform.position.z);
    }

} // class
