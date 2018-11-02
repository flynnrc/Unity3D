using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] GameObject deathFX;
    [SerializeField] Transform parentTransform;

    ScoreBoard scoreBoard;
    [SerializeField] int pointsForKilling;
    [SerializeField] int hp = 10;

    // Use this for initialization
    void Start ()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        AddBoxCollider();
    }

    private void AddBoxCollider()
    {
        Collider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = false;
    }

    // Update is called once per frame
    void Update () {
		
	}

    void OnParticleCollision(GameObject other)
    {
        //todo consider hit effects
        hp--;
        if (hp <= 0)
        {
            //print("particles collided with " + gameObject.name);
            GameObject fx = Instantiate(deathFX, gameObject.transform.position, Quaternion.identity);
            fx.transform.parent = parentTransform;
            Destroy(gameObject);
            scoreBoard.AddScoreByAmount(pointsForKilling);
        }

    }
}
