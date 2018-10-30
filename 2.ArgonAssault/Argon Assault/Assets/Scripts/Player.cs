﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class Player : MonoBehaviour {

    [Tooltip("In m^s-1")] [SerializeField] float xSpeed = 10f;
    [Tooltip("In m^s-1")] [SerializeField] float ySpeed = 10f;
    [SerializeField] float xRange = 5f;
    [SerializeField] float yRange = 3f;

    //pitch
    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float controlPitchFactor = -20f;
    //yaw
    [SerializeField] float positionYawFactor = 5f;
    //roll
    [SerializeField] float rollDueToControlThrow = -20f;

    float xThrow;
    float yThrow;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        ProcessTranslation();
        ProcessRotation();
    }

    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControlThrow;

        float yawDueToPosition = transform.localPosition.x * positionYawFactor;
        float yaw = yawDueToPosition;

        float roll = xThrow * rollDueToControlThrow;
        //order matters when setting rotation
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslation()
    {
        //edit > project settings > input > "String"
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");


        float xOffsetThisFrame = xThrow * xSpeed * Time.deltaTime;//xThrow(avg .66) * 80  frames = 52.8
        float yOffsetThisFrame = yThrow * ySpeed * Time.deltaTime;

        float rawNewXPos = transform.localPosition.x + xOffsetThisFrame;
        float rawNewYPos = transform.localPosition.y + yOffsetThisFrame;

        var clampedXPos = Mathf.Clamp(rawNewXPos, -xRange, xRange);
        var clampedYPos = Mathf.Clamp(rawNewYPos, -yRange, yRange);


        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }
}
