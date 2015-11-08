using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CoffeeScriptHueHueHue : MonoBehaviour {

	public Sprite[] coffeeProgressSprites;
	public GameObject panelObj;
	public Text gameOverTextObj;
	public Color gameOverColor;
	private static Color panelColor;
	private static Sprite[] sprites;
	private static SpriteRenderer sr;
	private static Image canvasImage;
	private static Text gameOverText;

	private static int beanCount = 0;


	// Use this for initialization
	void Start () {
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
		sr.sprite = sprites[beanCount];
		if(beanCount == sprites.Length - 1){
			PlayerPrefs.SetInt("Score", Score.GetScore());
			PlayerPrefs.SetInt("Level", Score.GetLevel() + 1);
			RestartGame();
		}
		beanCount++;
	}

	public static void ReceiveEnemy(){
		//you lose the game
		PlayerPrefs.DeleteKey("Score");
		canvasImage.color = panelColor;
		gameOverText.enabled = true;
		Time.timeScale = 0;
	}

}
