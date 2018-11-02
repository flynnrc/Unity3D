using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour {

    int score = 0;
    Text scoreText;

	// Use this for initialization
	void Start () {
        scoreText = GetComponent<Text>();
        scoreText.text = score.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddScoreByAmount(int incomingScoreValue)
    {
        score = score += incomingScoreValue;
        scoreText.text = score.ToString();
    }
}
