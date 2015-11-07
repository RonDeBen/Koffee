﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CoffeeScriptHueHueHue : MonoBehaviour {

	public Sprite[] coffeeProgressSprites;
	public GameObject panelObj;
	public Text gameOverTextObj;
	public Color gameOverColor;
    public static CoffeeScriptHueHueHue coffeePot;
	private static Color panelColor;
	private static Sprite[] sprites;
	private static SpriteRenderer sr;
	private static Image canvasImage;
	private static Text gameOverText;

	private static int beanCount = 1;


	// Use this for initialization
	void Start () {
        if(coffeePot != null)
        {
            GameObject.Destroy(gameObject);
        }
        else
        {
            coffeePot = this;
        }
		sr = gameObject.GetComponent<SpriteRenderer>();
		sprites = coffeeProgressSprites;
		panelColor = gameOverColor;
		canvasImage = panelObj.GetComponent<Image>();
		gameOverText = gameOverTextObj;
	}
	
	// Update is called once per frame
	void Update () {
		if (gameOverText.enabled && Input.GetKeyDown(KeyCode.R)){
			RestartGame();
		}
	}

	public static void RestartGame(){
		beanCount = 0;
		Application.LoadLevel("final");
		Time.timeScale = 1;
	}

	public static void ReceiveBean(){
        if (beanCount == sprites.Length + 1)
        {
            PlayerPrefs.SetInt("Score", Score.GetScore());
            PlayerPrefs.SetInt("Level", Score.GetLevel() + 1);
            RestartGame();
        }
        else
        {
            sr.sprite = sprites[beanCount - 1];
        }
        Debug.Log(beanCount);
		
		beanCount++;
        //Debug.Break();
	}

	public static void ReceiveEnemy(){
		//you lose the game
		PlayerPrefs.DeleteKey("Score");
		canvasImage.color = panelColor;
		gameOverText.enabled = true;
		Time.timeScale = 0;
	}

}
