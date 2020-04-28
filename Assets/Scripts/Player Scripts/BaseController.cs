﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    public Vector3 speed;
    //          left and right,  forward speed
    public float x_Speed = 8f, z_Speed = 15f;

    //          when we move faster,  when we break down
    public float accelerated = 15f, deccelarated = 10f; 
    
    protected float rotationSpeed = 10f; // it will be accacable in the child class
    protected float maxAngle = 10f; // maximum rotation angle

    public float low_Sound_Pitch, normal_Sound_Pitch, high_Sound_Pitch;

    public AudioClip engine_On_Sound, engine_Off_Sound;
    private bool is_Slow; // if we are going slow

    private AudioSource soundManager;

    private void Awake()
    {
        soundManager = GetComponent<AudioSource>();
        print("start awake speed: " + speed.ToString());
        speed = new Vector3(0f, 0f, z_Speed);
        print("end awake speed: " + speed.ToString());
    }

    protected void MoveLeft()
    {
        speed = new Vector3(-x_Speed / 2f, 0f, speed.z);
    }

    protected void MoveRight()
    {
        speed = new Vector3(x_Speed / 2f, 0f, speed.z);
    }

    protected void MoveStraight()
    {
        speed = new Vector3(0f, 0f, speed.z);
    }

    protected void MoveNormal()
    {
        if (is_Slow)
        {
            is_Slow = false;

            soundManager.Stop();
            soundManager.clip = engine_On_Sound;
            soundManager.volume = 0.3f;
            soundManager.Play();
        }
        speed = new Vector3(speed.x, 0f, z_Speed);
    }

    protected void MoveSlow()
    {
        if (!is_Slow)
        {
            is_Slow = true;

            soundManager.Stop();
            soundManager.clip = engine_Off_Sound;
            soundManager.volume = 0.5f;
            soundManager.Play();
        }
        speed = new Vector3(speed.x, 0f, deccelarated);
    }

    protected void MoveFast()
    {
        speed = new Vector3(speed.x, 0f, accelerated);
    }

} // class
