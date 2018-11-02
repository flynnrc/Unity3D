using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class PlayerController : MonoBehaviour {

    [Header("General")]
    [Tooltip("In m^s-1")] [SerializeField] float xControlSpeed = 10f;
    [Tooltip("In m^s-1")] [SerializeField] float yControlSpeed = 10f;
    [Tooltip("In m")] [SerializeField] float xRange = 5f;
    [Tooltip("In m")] [SerializeField] float yRange = 3f;

    [Header("Screen Position")]
    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float positionYawFactor = 5f;

    [Header("Control-Throw")]
    [SerializeField] float controlPitchFactor = -20f;
    [SerializeField] float rollDueToControlThrow = -20f;

    [SerializeField] GameObject[] guns;

    float xThrow;
    float yThrow;

    bool controlsAreEnabled = true;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (controlsAreEnabled) {
            ProcessTranslation();
            ProcessRotation();
            ProcessFiring();
        }
    }

    private void ProcessFiring()
    {
        if (CrossPlatformInputManager.GetButton("Fire"))
        {
            //print("Firing");
            ActivateGuns();
        }
        else
        {
            DeactivateGuns();
        }
    }

    private void ActivateGuns()
    {
        foreach (var gun in guns)
        {
            gun.SetActive(true);
        }
    }

    private void DeactivateGuns()
    {
        foreach (var gun in guns)
        {
            gun.SetActive(false);
        }
    }

    void OnPlayerDeath()//called by string reference
    {
        controlsAreEnabled = false;
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


        float xOffsetThisFrame = xThrow * xControlSpeed * Time.deltaTime;//xThrow(avg .66) * 80  frames = 52.8
        float yOffsetThisFrame = yThrow * yControlSpeed * Time.deltaTime;

        float rawNewXPos = transform.localPosition.x + xOffsetThisFrame;
        float rawNewYPos = transform.localPosition.y + yOffsetThisFrame;

        var clampedXPos = Mathf.Clamp(rawNewXPos, -xRange, xRange);
        var clampedYPos = Mathf.Clamp(rawNewYPos, -yRange, yRange);


        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }
}
