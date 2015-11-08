using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour {

public Text scoreText;
private static int score;

	void Update(){
		scoreText.text = "Score: " + score;
	}

	public static void AddPoints(int value){
		score += value;
	}
}
