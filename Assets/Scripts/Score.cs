using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour {

public Text scoreText, levelText;
private static int score, level;

	void Start(){
		score = PlayerPrefs.GetInt("Score");
		level = PlayerPrefs.GetInt("Level");

		if (level == 0){
			level = 1;
			PlayerPrefs.SetInt("Level", 1);
		}
	}

	void Update(){
		scoreText.text = "Score: " + score;
		levelText.text = "Level: " + level;
	}


	public static void AddPoints(int value){
		score += value;
	}

	public static int GetScore(){
		return score;
	}

	public static int GetLevel(){
		return level;
	}
}
