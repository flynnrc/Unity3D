using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour {

    int score = 0;
    Text scoreText;
    [SerializeField] int defaultScorePerHit = 12;

	// Use this for initialization
	void Start () {
        scoreText = GetComponent<Text>();
        scoreText.text = score.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DefaultScoreHit()
    {
        score = score + defaultScorePerHit;
        scoreText.text = score.ToString();
    }
}
