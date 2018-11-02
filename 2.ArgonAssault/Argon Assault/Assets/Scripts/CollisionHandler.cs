using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //okay as long as it's the only script that loads scenes

public class CollisionHandler : MonoBehaviour {

    [Tooltip("In Seconds")][SerializeField] float levelLoadDelay = 3f;
    [Tooltip("FX On Player")] [SerializeField] GameObject deathFx;
 
    private void OnCollisionEnter(Collision collision)
    {
        //print("collision detected");
    }
    private void OnTriggerEnter(Collider other)
    {
        //print("triggered");
        StartDeathSequence();
        deathFx.SetActive(true);
        Invoke("ReloadScene", levelLoadDelay);
    }

    private void StartDeathSequence()
    {
        SendMessage("OnPlayerDeath");
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(1);
    }
}
