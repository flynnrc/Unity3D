using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] GameObject deathFX;
    [SerializeField] Transform parentTransform;

	// Use this for initialization
	void Start () {
        Collider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = false;
    }

    // Update is called once per frame
    void Update () {
		
	}

    void OnParticleCollision(GameObject other)
    {
        print("particles collided with " + gameObject.name);
        GameObject fx = Instantiate(deathFX, gameObject.transform.position, Quaternion.identity);
        fx.transform.parent = parentTransform;
        Destroy(gameObject);
    }
}
