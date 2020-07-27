﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationEvents : MonoBehaviour
{
    private PlayerController playerController;
    private Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
    }

    void ResetShooting()
    {
        playerController.canShoot = true;
        anim.Play("Idle");
    }

    void CameraStartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
