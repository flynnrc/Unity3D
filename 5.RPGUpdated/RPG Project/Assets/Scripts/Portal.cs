using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    enum DestinationIdentifier
    {
        A, B, C, D, E
    }

    [SerializeField] int sceneIndexToLoad = -1;
    [SerializeField] Transform spawnPoint;
    [SerializeField] DestinationIdentifier destination;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            StartCoroutine(Transition());
        }
    }

    private IEnumerator Transition()
    {
        if(sceneIndexToLoad < 0)
        {
            //no scene indexes below zero, need to fix in editor
            Debug.LogError("Scene to load not set.");
            yield break;
        }

        DontDestroyOnLoad(gameObject);//don't destroy onload only works at root level of the scene
        yield return SceneManager.LoadSceneAsync(sceneIndexToLoad);
        //print("Scene Loaded");

        Portal otherPortal = GetOtherPortal();
        UpdatePlayerPosition(otherPortal);

        Destroy(gameObject);
    }

    private void UpdatePlayerPosition(Portal otherPortal)
    {
        GameObject player = GameObject.FindWithTag("Player");
        player.transform.position = otherPortal.spawnPoint.position;
        player.transform.rotation = otherPortal.spawnPoint.rotation;
    }

    private Portal GetOtherPortal()
    {
        foreach(var portal in FindObjectsOfType<Portal>())
        {
            if (portal == this || portal.destination != destination) { continue; }
            return portal;
        }

        return null;
    }
}
